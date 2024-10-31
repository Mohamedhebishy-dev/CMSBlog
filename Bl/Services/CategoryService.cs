using Bl.Contracts;
using Domain;
using Domain.Reposotry;
namespace Bl.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IReposotry<Category> _genericRepositoryCategory;
        private readonly IReposotry<Article> _genericRepository;
        public CategoryService(IReposotry<Category> genericRepository, IReposotry<Article> reposotry) : base(genericRepository)
        {
            _genericRepository =reposotry;
            _genericRepositoryCategory =genericRepository;
        }

    }
}
