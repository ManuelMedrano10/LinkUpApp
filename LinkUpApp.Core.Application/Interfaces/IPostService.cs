using LinkUpApp.Core.Application.Dtos.Post;
using LinkUpApp.Core.Application.ViewModels.Post;

namespace LinkUpApp.Core.Application.Interfaces
{
    public interface IPostService : IGenericService<PostDto, SavePostDto>
    {
        Task<List<PostDto>> GetAllByUserIdAsync(string userId);
        Task<List<PostDto>> GetAllFromFriendsAsync(string userId);
        Task ToggleReactionAsync(int postId, string userId, bool isLike);
    }
}
