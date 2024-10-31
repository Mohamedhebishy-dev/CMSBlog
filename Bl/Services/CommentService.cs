using Domain;
using Domain.Reposotry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        private readonly IReposotry<Comment> _genericRepository;
        private readonly IComment _CommentRepository;
        public CommentService(IReposotry<Comment> genericRepository, IComment commentRepository) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _CommentRepository = commentRepository;
        }

        public Task<List<Comment>> GetAllWithIncludeAsync(params Expression<Func<Comment, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetCommentsByArticle(Guid AricleId)
        {
     return  _CommentRepository.GetCommentsByArticle(AricleId);
        }
    }
}
