using Bl.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CMSBlog.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Categories = await _categoryService.GetAllAsync();
            return View(Categories);
        }
    }
}
