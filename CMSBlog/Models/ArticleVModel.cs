using Domain;
using System.ComponentModel.DataAnnotations;

namespace CMSBlog.Models
{
    public class ArticleVModel
    {

        public Article Article { get; set; }
        public List<Comment> comments { get; set; }
        public CommentViewModel NewComment { get; set; }
    }
}
