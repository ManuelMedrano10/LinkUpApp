using LinkUpApp.Core.Application.Dtos.Comment;

namespace LinkUpApp.Core.Application.Dtos.Reply
{
    public class SaveReplyDto
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public required string UserId { get; set; }
        public required int CommetId { get; set; }
        public CommentDto? Comment { get; set; }
    }
}
