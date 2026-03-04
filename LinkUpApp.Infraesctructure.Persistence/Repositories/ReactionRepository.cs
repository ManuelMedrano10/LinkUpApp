using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class ReactionRepository : GenericRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
