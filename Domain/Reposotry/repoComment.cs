using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Reposotry
{
    public class repoComment : IComment
    {
        BlogContext BlogContext;
        public repoComment(BlogContext blogContext)
        {

            BlogContext = blogContext;

        }
        public void Create(Comment model)
        {
            BlogContext.Comments.Add(model);
            BlogContext.SaveChanges();
        }

        public void DeleteById(Guid Id)
        {
            var comment = GetById(Id);
            BlogContext?.Comments.Remove(comment);
        }

        public List<Comment> GetAll()
        {
            return BlogContext.Comments.ToList();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllWithIncludeAsync(params Expression<Func<Comment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Comment GetById(Guid Id)
        {
            return BlogContext.Comments.FirstOrDefault(a => a.CommentId == Id);
        }

        public Comment GetByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByArticle(Guid AricleId)
        {
          
            return BlogContext.Comments.Where(a=>a.ArticleId== AricleId).ToList();
        }

        public void Update(Comment model)
        {
            var comment = GetById(model.CommentId);
            comment.CommentId = model.CommentId;
            comment.CommentContent = model.CommentContent;
            comment.Articles.ArticleId = model.Articles.ArticleId;
            BlogContext.SaveChanges();
        }
    }
}
