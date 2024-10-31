namespace Domain.Reposotry
{
    public interface IArticle : IReposotry<Article>
    {
        Article GetByUrl(string url);
        //public List<Article> GetAllByCategory(string url);
    }
}
