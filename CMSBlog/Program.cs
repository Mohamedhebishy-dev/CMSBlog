using Bl.Contracts;
using Bl.Services;
using Domain;
using Domain.Reposotry;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace CMSBlog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<BlogContext>();

            builder.Services.AddScoped<IArticle,repoArticle>();
            // builder.Services.AddScoped<ICategory,repoCategory>();
            // builder.Services.AddScoped<IComment,repoComment>();
            builder.Services.AddScoped(typeof(IReposotry<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IComment,repoComment>(); 
            builder.Services.AddScoped<IArticleService, AricleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICommentService,CommentService > ();

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Home/AccessDenied";
                options.Cookie.Name = "Cookie";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.B
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
        
  
            app.MapControllerRoute(
               name: "areas",
               pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
              name: "category",
              pattern: "category/{url}", // {categoryUrl} ���� ����� URL �����
              defaults: new { controller = "Category", action = "Index" });


            app.MapControllerRoute(
          name: "article",
          pattern: "article/{url}", // {url} ���� ����� URL ������
          defaults: new { controller = "Article", action = "Index" });


     





            app.Run();

        }
    }
}