using LinkUpApp.Core.Application.Dtos.Friendship;
using LinkUpApp.Core.Application.ViewModels.Friendship;

namespace LinkUpApp.Core.Application.Interfaces
{
    public interface IFrienshipService : IGenericService<FriendshipDto>
    {
        Task<List<FriendshipDto>> GetPendingRequestsAsync(string currentUserId);
        Task<List<FriendshipDto>> GetSentRequestsAsync(string currentUserId);
        Task<List<FriendshipDto>> GetFriendsAsync(string currentUserId);
        Task AcceptRequestAsync(int friendshipId);
        Task RejectRequestAsync(int friendshipId);
        Task DeleteFriendAsync(int friendshipId);
    }
}
