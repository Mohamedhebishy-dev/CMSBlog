using Domain;

namespace CMSBlog.Models
{
    public class CategoryArticlesHomeView
    {

        public Category category { get; set; }
        public List<Article> Articles { get; set; }
    }
}
