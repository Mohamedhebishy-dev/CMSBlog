using Bl.Contracts;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace CMSBlog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        public CategoryController(ICategoryService categoryService,IArticleService articleService)
        {
           _categoryService = categoryService;
            _articleService = articleService;
        }

     
        public IActionResult Index()
        {
          var Categories =  _categoryService.GetAll();
            return View(Categories);
        }
        [HttpGet("category/{Url}")]
        public  async Task< IActionResult> Details(string url)
        {

            var articles = await _articleService.GetAllArticleByCategoryAsync(url);
            if(articles == null)
            {
                return NotFound();
            }


            return View(articles);
        }
    }
}
