namespace LinkUpApp.Core.Application.Dtos.Comment
{
    public class SaveCommentDto
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public required int PostId { get; set; }
        public required string UserId { get; set; }
    }
}
