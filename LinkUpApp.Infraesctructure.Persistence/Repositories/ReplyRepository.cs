using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class ReplyRepository : GenericRepository<Reply>, IReplyRepository
    {
        public ReplyRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
