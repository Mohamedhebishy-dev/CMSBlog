using Domain;

namespace Bl.Contracts
{
    public interface IArticleService : IBaseService<Article>
    {
        Task<List<Article>> GetAllArticlesWithCategoryAsync();
        Article GetByUrl(string url);
        Task<List<Article>> GetAllArticleByCategoryAsync(string Url);
    }
}
