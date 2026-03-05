using LinkUpApp.Core.Application.Dtos.Comment;

namespace LinkUpApp.Core.Application.Dtos.Post
{
    public class PostDto
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string UserPhotoUrl { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool? IsLikedByCurrentUser { get; set; } 
        public ICollection<CommentDto>? Comments { get; set; }
    }
}
