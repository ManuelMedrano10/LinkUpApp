using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class ShipPositionRepository : GenericRepository<ShipPosition>, IShipPositionRepository
    {
        public ShipPositionRepository(LinkUpContext context) : base(context)
        {
        }
    }
}
