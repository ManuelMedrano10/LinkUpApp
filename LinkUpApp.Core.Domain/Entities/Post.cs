namespace LinkUpApp.Core.Domain.Entities
{
    public class Post
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }

        //Navigations properties
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
