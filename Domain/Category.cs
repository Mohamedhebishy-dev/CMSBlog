using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Category : SecurityBase
    {
        public Category()
        {
            Articles = new List<Article>();
        }
        [Required]
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string CategoryUrl { get; set; }
        public List<Article> Articles { get; set; }
    }
}
