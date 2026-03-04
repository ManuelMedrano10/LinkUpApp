using LinkUpApp.Core.Application.Dtos.Post;

namespace LinkUpApp.Core.Application.Dtos.Reaction
{
    public class SaveReactionDto
    {
        public required int Id { get; set; }
        public bool IsLiked { get; set; }
        public required string UserId { get; set; }
        public required int PostId { get; set; }
        public PostDto? Post { get; set; }
    }
}
