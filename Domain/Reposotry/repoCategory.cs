using System.Linq.Expressions;

namespace Domain.Reposotry
{
    public class repoCategory : ICategory
    {
        BlogContext BlogContext;
        public repoCategory(BlogContext blogContext)
        {

            BlogContext = blogContext;

        }
        public void Create(Category model)
        {
            BlogContext.Categories.Add(model);
            BlogContext.SaveChanges();
        }

        public void DeleteById(Guid Id)
        {
            var Entity = GetById(Id);
            BlogContext.Categories.Remove(Entity);
            BlogContext.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return BlogContext.Categories.ToList();
        }

        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllWithIncludeAsync(params Expression<Func<Category, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Category GetById(Guid Id)
        {
            return BlogContext.Categories.FirstOrDefault(a => a.CategoryId == Id);
        }

        public Category GetByUrl(string url)
        {
            throw new NotImplementedException();
        }

        public void Update(Category model)
        {
            var oldEntity = GetById(model.CategoryId);
            oldEntity.CategoryId = model.CategoryId;
            oldEntity.CategoryName = model.CategoryName;
            oldEntity.CategoryUrl = model.CategoryUrl;
            BlogContext.SaveChanges();
        }
    }
}
