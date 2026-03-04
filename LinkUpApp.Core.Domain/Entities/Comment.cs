namespace LinkUpApp.Core.Domain.Entities
{
    public class Comment
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }

        //Navigation Properties
        public required int PostId { get; set; }
        public Post? Post { get; set; }
        public ICollection<Reply>? Replies { get; set; }
    }
}
