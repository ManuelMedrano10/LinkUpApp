using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class ShotRepository : GenericRepository<Shot>, IShotRepository
    {
        public ShotRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
