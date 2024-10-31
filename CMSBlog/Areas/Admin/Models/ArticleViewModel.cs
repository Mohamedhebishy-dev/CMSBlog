namespace CMSBlog.Areas.Admin.Models
{
    public class ArticleViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string ContentHtml { get; set; }
       public List<IFormFile> Files { get; set; }
        public Guid CategoryId { get; set; }
    }
}
