using Bl.Contracts;
using Bl.Services;
using CMSBlog.Areas.Admin.Models;
using CMSBlog.Resources;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        public DashboardController(UserManager<ApplicationUser> userManager,ICategoryService categoryService,IArticleService articleService)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _articleService = articleService;
        }
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            List<ReportCardsView> list = new List<ReportCardsView>();
            ReportCardsView UsersCard = new ReportCardsView()
            {
               Title = ResAdmin.lblUsers,
               Count = _userManager.Users.Count(),
               BackgroundColor = "bg-success",
               Icon= "fas fa-user-plus",
               Link="Article"

            };

            ReportCardsView Articles = new ReportCardsView()
            { 
                Count=_articleService.GetAll().Count,
                Title=ResAdmin.lblArticles,
                BackgroundColor = "bg-info",
                Icon = "fas fa-pencil-alt",
                Link = "Article"
            };
            ReportCardsView categories = new ReportCardsView()
            {
                Count = _categoryService.GetAll().Count,
                Title = ResAdmin.lblCategories,
                BackgroundColor = "bg-primary",
                Icon = "fas fa-tags",
                Link = "Category"
            };
            list.Add(UsersCard);
            list.Add(Articles);
            list.Add(categories);
            return View(list);
        }
    }
}
