using System.Linq.Expressions;

namespace Domain.Reposotry
{
    public interface IReposotry<T>
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(Guid Id);
        void Create(T model);
        void Update(T model);
        void DeleteById(Guid Id);
        Task<List<T>> GetAllWithIncludeAsync(params Expression<Func<T, object>>[] includes);
    


    }
}
