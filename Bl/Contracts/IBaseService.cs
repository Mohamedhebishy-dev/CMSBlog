namespace Bl.Contracts
{
    public interface IBaseService<T>
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(Guid Id);
        void Create(T model);
        void Update(T model);
        void DeleteById(Guid Id);
       
    }
}
