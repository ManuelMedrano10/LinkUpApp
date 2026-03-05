namespace LinkUpApp.Core.Application.ViewModels.Battleship
{
    public class GameHistoryViewModel
    {
        public int GameId { get; set; }
        public required string OpponentName { get; set; }
        public int Status { get; set; }
        public bool IsWinner { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
