using Bl.Contracts; // استخدم namespace المناسب لخدمة categoryService
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace CMSBlog.Helpers
{

    [HtmlTargetElement("categories-dropdown")]
    public class CategoriesDropdownTagHelper : TagHelper
    {
        private readonly ICategoryService _categoryService;

        public CategoriesDropdownTagHelper(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var categories = await _categoryService.GetAllAsync(); // فرضية أن GetAllCategoriesAsync موجودة

            // إعداد العنصر HTML لإدراج قائمة التصنيفات
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "navbar-nav");

            string content = "";
            foreach (var category in categories)
            {
                content += $"<li class='nav-item'><a class='nav-link' href='#'>{category.CategoryName}</a></li>";
            }

            output.Content.SetHtmlContent(content);
        }
    }
}
