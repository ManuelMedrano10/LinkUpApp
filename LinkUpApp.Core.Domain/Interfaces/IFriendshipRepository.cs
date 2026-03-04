using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Domain.Interfaces
{
    public interface IFriendshipRepository : IGenericRepository<Friendship>
    {
        Task<List<Friendship>> GetAllFriendshipsByUserIdAsync(string userId);
        Task<bool> FriendshipExistsAsync(string senderUserId, string receiverUserId);
    }
}
