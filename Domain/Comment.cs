using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Comment : SecurityBase
    {
        public Comment()
        {

        }
        [Key]
        public Guid CommentId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CommentContent { get; set; }
        [Required]
        public Guid ArticleId { get; set; }
        [ForeignKey(nameof(Article.ArticleId))]
        public Article Articles { get; set; }

    }
}
