using Domain;

namespace CMSBlog.Models
{
    public class HomeModelView
    {
        public List<CategoryArticlesHomeView> CategoryArticles { get; set; }
         public List<Article> Articles { get; set; }
 
    }
}
