using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Article :SecurityBase
    {
        public Article()
        {
            Category = new Category();
            comments = new List<Comment>();
        }
        [Required]
        [Key]
        public Guid ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ContentHtml { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public  string ImageName { get; set; }
        public Category Category { get; set; }

        public List<Comment> comments { get; set; }


    }
}
