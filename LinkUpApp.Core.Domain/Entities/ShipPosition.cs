namespace LinkUpApp.Core.Domain.Entities
{
    public class ShipPosition
    {
        public required int Id { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public required string PlayerId { get; set; }
        public int Size { get; set; }
        public bool IsHorizontal { get; set; }
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int Hits { get; set; } = 0;
        public bool IsSunk => Hits >= Size;
    }
}
