namespace LinkUpApp.Core.Domain.Entities
{
    public class Reply
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public required string UserId { get; set; }

        //Navigation Property
        public required int CommetId { get; set; }
        public Comment? Comment { get; set; }

    }
}
