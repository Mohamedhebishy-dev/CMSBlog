using System.Linq.Expressions;

namespace Domain.Reposotry
{
    public class repoArticle : IArticle
    {

        public repoArticle(BlogContext blogCoontext)
        {
            BlogContext = blogCoontext;

        }
        BlogContext BlogContext;

        public List<Article> GetAll()
        {
            return BlogContext.Articles.ToList();
        }

        public Article GetById(Guid Id)
        {
            return BlogContext.Articles.FirstOrDefault(a => a.ArticleId == Id);
        }
        public void Create(Article model)
        {
            BlogContext.Add(model);
            BlogContext.SaveChanges();

        }

        public void DeleteById(Guid Id)
        {
            var entity = GetById(Id);
            BlogContext.Articles.Remove(entity);
            BlogContext.SaveChanges();
        }



        public void Update(Article model)
        {
            var oldEntity = GetById(model.ArticleId);
            oldEntity.ArticleId = model.ArticleId;
            oldEntity.Category = model.Category;
            oldEntity.Title = model.Title;
            oldEntity.ContentHtml = model.ContentHtml;

        }

        public Task<List<Article>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Article>> GetAllWithIncludeAsync(params Expression<Func<Article, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Article GetByUrl(string url)
        {
return BlogContext.Articles.FirstOrDefault(a=>a.Url == url);
        }

        //public List<Article> GetAllByCategory(string url)
        //{
        // return  BlogContext.Articles.FirstOrDefault(a=>a.Category.CategoryUrl == url);
        //}
    }
}
