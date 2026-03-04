namespace LinkUpApp.Core.Domain.Entities
{
    public class Reaction
    {
        public required int Id { get; set; }
        public bool IsLiked { get; set; }
        public required string UserId { get; set; }

        //Navigation Property
        public required int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
