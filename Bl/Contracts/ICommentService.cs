namespace Domain.Reposotry
{
    public interface ICommentService : IReposotry<Comment>
    {
        List<Comment> GetCommentsByArticle(Guid AricleId);
    }
}
