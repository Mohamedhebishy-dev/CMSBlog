namespace Bl.Contracts
{
    public interface IServices<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid Id);
        void Create(T model);
        void Update(T model);
        void DeleteById(Guid Id);
    }
}
