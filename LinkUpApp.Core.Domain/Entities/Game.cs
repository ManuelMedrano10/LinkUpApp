namespace LinkUpApp.Core.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Player1Id { get; set; }
        public required string Player2Id { get; set; }
        public int Status { get; set; } = 1;
        public string? CurrentTurnPlayerId { get; set; }
        public string? WinnerId { get; set; }
        public bool Player1Ready { get; set; } = false;
        public bool Player2Ready { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<ShipPosition> Ships { get; set; } = [];
        public ICollection<Shot> Shots { get; set; } = [];
    }
}
