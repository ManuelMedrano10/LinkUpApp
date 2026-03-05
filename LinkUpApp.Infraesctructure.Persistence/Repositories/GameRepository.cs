using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        private readonly LinkUpContext _context;

        public GameRepository(LinkUpContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Game?> GetGameWithDetailsAsync(int gameId)
        {
            return await _context.Set<Game>()
                .Include(g => g.Ships)
                .Include(g => g.Shots)
                .FirstOrDefaultAsync(g => g.Id == gameId);
        }
    }
}
