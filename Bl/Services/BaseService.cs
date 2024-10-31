using Bl.Contracts;
using Domain.Reposotry;

namespace Bl.Services
{
    public class BaseService<T> : IBaseService<T>
    {
        private readonly IReposotry<T> _repository;
        public BaseService(IReposotry<T> genericRepository)
        {
            _repository = genericRepository;
        }
        public List<T> GetAll()
        {
            return _repository.GetAll();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public T GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        public void DeleteById(Guid id)
        {
            _repository.DeleteById(id);
        }
        public void Update(T model)
        {
            _repository.Update(model);
        }

        public void Create(T model)
        {
            _repository.Create(model);
        }

    }
}
