using Bl.Contracts;
using Domain;
using Domain.Reposotry;
using System;



namespace Bl.Services
{
    public class AricleService : BaseService<Article>, IArticleService
    {
        private readonly IReposotry<Article> _genericRepository;
        private readonly IArticle _Article;


        public AricleService(IReposotry<Article> genericRepository, IArticle article) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _Article = article;
        }
        public async Task<List<Article>> GetAllArticlesWithCategoryAsync()
        {
            return await _genericRepository.GetAllWithIncludeAsync(a => a.Category);
        }
      public Article  GetByUrl(string url)
        {
         return   _Article.GetByUrl(url);
        }
 
        public async Task< List<Article>> GetAllArticleByCategoryAsync(string Url)
        {
            var allArticleWithCategories = await GetAllArticlesWithCategoryAsync();
            var filter = allArticleWithCategories.Select(a => a.Category).FirstOrDefault(a => a.CategoryUrl == Url);
        if (filter != null)
            {
                var articles = filter.Articles.ToList();
                return articles;
            }
        
            return new List<Article>();

        }
    }
}
