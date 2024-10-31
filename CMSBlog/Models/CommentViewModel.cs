using System.ComponentModel.DataAnnotations;

namespace CMSBlog.Models
{
    public class CommentViewModel
    {
        [Required]
        public Guid ArticleId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CommentContent { get; set; }

        public  string? ArticleUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
    
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
