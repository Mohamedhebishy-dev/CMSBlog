using Bl.Contracts;
using CMSBlog.Areas.Admin.Helpers;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CMSBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
   
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _CategoryService;
        public CategoryController(ICategoryService categoryService,UserManager<ApplicationUser> userManager)
        {
          
            _userManager = userManager;
            _CategoryService = categoryService;
        }
        public IActionResult Index()
        {
           

            var Categories = _CategoryService.GetAll();
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Add(Category category)
        {
            try
            {

                var id = Guid.Parse(_userManager.GetUserId(User));
                category.CategoryName = category.CategoryName;
                category.CreatedDate = DateTime.Now;
                category.CreatedBy = id;


                _CategoryService.Create(category);
                TempData["MessageType"] = MessageType.SaveSuccess;
            }catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.SaveFailed;
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(Guid CategoryId)
        {
           var category = _CategoryService.GetById(CategoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category Category)
        {
            try { 
            var id = Guid.Parse(_userManager.GetUserId(User));
            var old = _CategoryService.GetById(Category.CategoryId);
            old.CategoryName = Category.CategoryName;
            old.UpdatedBy= id;
            old.UpdatedDate = DateTime.Now;
            old.CategoryUrl = Category.CategoryUrl;
            _CategoryService.Update(old);
            TempData["MessageType"] = MessageType.UpdataSuccess;
        }catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.UpdataFailed;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(Guid CategoryId)
        {
            try
            {
                _CategoryService.DeleteById(CategoryId);
                TempData["MessageType"] = MessageType.DeleteSuccess;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["MessageType"] = MessageType.DeleteFailed;
            }
            return RedirectToAction("Index");
        }
    }
}
