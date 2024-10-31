using Bl.Contracts;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        
            var id = Guid.Parse(_userManager.GetUserId(User));
            category.CategoryName = category.CategoryName;
            category.CreatedDate = DateTime.Now;
             category.CreatedBy = id;


            _CategoryService.Create(category);
            return RedirectToAction("Index");
        }
    }
}
