namespace LinkUpApp.Core.Application.Dtos.Post
{
    public class SavePostDto
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? YoutubeUrl { get; set; }
        public required string UserId { get; set; }
    }
}
