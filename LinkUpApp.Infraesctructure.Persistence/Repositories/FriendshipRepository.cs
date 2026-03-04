using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        private readonly LinkUpContext _context;

        public FriendshipRepository(LinkUpContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Friendship>> GetAllFriendshipsByUserIdAsync(string userId)
        {
            return await _context.Set<Friendship>()
                .Where(f => f.SenderUserId == userId || f.ReceiverUserId == userId)
                .ToListAsync();
        }

        public async Task<bool> FriendshipExistsAsync(string senderId, string receiverId)
        {
            return await _context.Set<Friendship>()
                .AnyAsync(f => (f.SenderUserId == senderId && f.ReceiverUserId == receiverId) ||
                               (f.SenderUserId == receiverId && f.ReceiverUserId == senderId));
        }
    }
}
