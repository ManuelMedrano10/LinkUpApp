using LinkUpApp.Core.Application.Dtos.Reply;

namespace LinkUpApp.Core.Application.Dtos.Comment
{
    public class CommentDto
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string UserImageUrl { get; set; }
        public List<ReplyDto>? Replies { get; set; }
    }
}
