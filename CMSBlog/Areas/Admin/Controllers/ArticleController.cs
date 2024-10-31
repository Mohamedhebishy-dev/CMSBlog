using Bl.Contracts;
using CMSBlog.Areas.Admin.Models;
using CMSBlog.Models;
using CMSBlog.Utlities;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService, ICategoryService categoryService,UserManager<ApplicationUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var Articles = await _articleService.GetAllArticlesWithCategoryAsync();
            return View(Articles);
        }
        public IActionResult Add()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Add(ArticleViewModel article,List<IFormFile> Files)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryService.GetById(article.CategoryId);
                var id = Guid.Parse(_userManager.GetUserId(User));
                Article newArticle = new Article()
                {
                    CreatedBy = id,
                    CreatedDate = DateTime.Now,
                    ArticleId = Guid.NewGuid(),
                    Title = article.Title,
                    Url = article.Url,
                    ContentHtml = article.ContentHtml,
                    Category = category,
                   
                };
                var ImageName = await Helper.UploadImage(Files, "Articles","Images");
                newArticle.ImageName = ImageName;
                _articleService.Create(newArticle);
                return RedirectToAction("Index");
            }
           return View(article);
        }

    }
}
