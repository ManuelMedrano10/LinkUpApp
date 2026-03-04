using LinkUpApp.Core.Application.ViewModels.Post;

namespace LinkUpApp.Core.Application.ViewModels.Reaction
{
    public class SaveReactionViewModel
    {
        public required int Id { get; set; }
        public bool IsLiked { get; set; }
        public required string UserId { get; set; }
        public required int PostId { get; set; }
        public PostViewModel? Post { get; set; }
    }
}
