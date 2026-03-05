namespace LinkUpApp.Core.Domain.Entities
{
    public class Shot
    {
        public required int Id { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public required string PlayerId { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsHit { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
