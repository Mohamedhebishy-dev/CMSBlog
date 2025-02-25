using Bl.Contracts;
using CMSBlog.Models;
using Domain;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
namespace CMSBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly ISiteSettingsService _siteSettingsService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService, ISiteSettingsService siteSettingsService, ICategoryService categoryService) : base()
        {
            _logger = logger;
            _articleService = articleService;
            _siteSettingsService = siteSettingsService;
            _categoryService = categoryService;
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
        public async Task< IActionResult> Index()
        {
            HomeModelView model = new HomeModelView();
         
            List<CategoryArticlesHomeView>categoryArticles = new List<CategoryArticlesHomeView>();
            var AllArticleByCategory = await  _articleService.GetAllArticleByCategoryAsync("fdfdf");
            var AllArticles = _articleService.GetAll();
            var settings = await _siteSettingsService.GetHomeSettingsAsync(2);
            var Categories= settings.value;
            string[] listCategories = Categories.Split(",");
            foreach(var strCategoryId in listCategories)
            {
                CategoryArticlesHomeView item = new CategoryArticlesHomeView();
                item.category = _categoryService.GetById(Guid.Parse(strCategoryId));
                item.Articles =await _articleService.GetAllArticleByCategoryAsync(item.category.CategoryUrl);
           
                categoryArticles.Add(item);

            }
            model.Articles = AllArticles;
            model.CategoryArticles = categoryArticles;
      
            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
