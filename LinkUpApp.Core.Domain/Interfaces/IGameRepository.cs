using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Domain.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game>
    {
        Task<Game?> GetGameWithDetailsAsync(int gameId);
    }
}
