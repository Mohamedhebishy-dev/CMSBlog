namespace Domain.Reposotry
{
    public interface IComment : IReposotry<Comment>
    {
        List<Comment> GetCommentsByArticle(Guid AricleId);
    }
}
