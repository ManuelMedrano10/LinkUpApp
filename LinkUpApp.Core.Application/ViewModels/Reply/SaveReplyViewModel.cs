using LinkUpApp.Core.Application.ViewModels.Comment;

namespace LinkUpApp.Core.Application.ViewModels.Reply
{
    public class SaveReplyViewModel
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public required string UserId { get; set; }
        public required int CommetId { get; set; }
        public CommentViewModel? Comment { get; set; }
    }
}
