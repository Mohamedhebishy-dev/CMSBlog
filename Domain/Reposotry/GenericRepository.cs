using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Reposotry
{
    public class GenericRepository<T> : IReposotry<T> where T : class
    {
        private readonly BlogContext BlogContext;
        private readonly DbSet<T> dbSet;
        public GenericRepository(BlogContext blogContext)
        {
            BlogContext = blogContext;
            dbSet = BlogContext.Set<T>();
        }
        public void Create(T model)
        {
            dbSet.Add(model);
            BlogContext.SaveChanges();
        }

        public void DeleteById(Guid Id)
        {
            dbSet.Remove(GetById(Id));
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync(); // استخدام ToListAsync بشكل صحيح
        }
        public T GetById(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Update(T model)
        {
            BlogContext.Entry(model).State = EntityState.Modified;
            BlogContext.SaveChanges();
        }

        public async Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            // إضافة Include للاستعلام
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

 
    }
}
