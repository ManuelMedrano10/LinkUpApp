using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
