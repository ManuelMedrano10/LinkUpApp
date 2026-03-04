using LinkUpApp.Core.Application.ViewModels.Comment;

namespace LinkUpApp.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string UserProfilePicture { get; set; }
        public List<CommentViewModel>? Comments { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool? CurrentUserReaction { get; set; }
    }
}
