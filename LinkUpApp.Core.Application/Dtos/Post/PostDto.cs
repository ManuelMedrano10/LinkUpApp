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
    }
}
