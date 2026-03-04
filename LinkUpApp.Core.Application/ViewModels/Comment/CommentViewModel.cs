using LinkUpApp.Core.Application.ViewModels.Reply;

namespace LinkUpApp.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string UserId { get; set; }
        public required string UserName { get; set; }
        public required string UserImageUrl { get; set; }
        public List<ReplyViewModel>? Replies { get; set; }
    }
}
