using Bl.Contracts;
using CMSBlog.Areas.Admin.Models;
using CMSBlog.Controllers;
using CMSBlog.Models;
using CMSBlog.Resources;
using CMSBlog.Utlities;
using Domain;
using Domain.Reposotry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using CMSBlog.Areas.Admin.Helpers;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize()]
    public class ArticleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IStringLocalizer _localizer;

        public ArticleController(IArticleService articleService, ICategoryService categoryService,UserManager<ApplicationUser> userManager, IStringLocalizerFactory localizerFactory)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
            var type = typeof(MessagesAR); // اسم الكلاس المولد من ملف resx
            _localizer = localizerFactory.Create(type);

        }
    

        public async Task<IActionResult> Index()
        {
            ViewBag.WelcomeMessage = _localizer["WelcomeMessage"];
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
                try { 
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
                    TempData["MessageType"] = MessageType.SaveSuccess;
               
                }
                catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.SaveFailed;
            }
            return RedirectToAction("Index");
            }
           return View(article);
        }

        [HttpGet]
        public IActionResult Edit(Guid ArticleId)
        {
            ViewBag.Categories = _categoryService.GetAll();
            var article =  _articleService.GetById(ArticleId);
            ArticleViewModel view = new ArticleViewModel() 
            {
                ContentHtml = article.ContentHtml,
                CategoryId = article.CategoryId,
                Url = article.Url,
                Title= article.Title,
                ImageName = article.ImageName,
                ArticleId=article.ArticleId,

            };
         return View(view);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ArticleViewModel article, List<IFormFile>? Files,Guid articleId)
        {
            if (ModelState.IsValid)
            {
                try { 
                var category = _categoryService.GetById(article.CategoryId);
                var id = Guid.Parse(_userManager.GetUserId(User));

                var oldArticle = _articleService.GetById(articleId);


                oldArticle.ArticleId = articleId;
                   oldArticle.Title = article.Title;
                  oldArticle.Url = article.Url;
                  oldArticle.ContentHtml = article.ContentHtml;
                   oldArticle.Category = category;

                    if (article.Files != null)
                    {
                        var ImageName = await Helper.UploadImage(Files, "Articles", "Images");
                        oldArticle.ImageName = ImageName;
                    }
         
                _articleService.Update(oldArticle);
                TempData["MessageType"] = MessageType.UpdataSuccess;
            }
                catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.UpdataFailed;
            }
            return RedirectToAction("Index");
            }
            return View(article);
        }

        public IActionResult Delete(Guid articleId)
        {
            try { 
            _articleService.DeleteById(articleId);
            TempData["MessageType"] = MessageType.DeleteSuccess;
        }
                catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.DeleteFailed;
            }
            return RedirectToAction("Index");
        }

    }
}
