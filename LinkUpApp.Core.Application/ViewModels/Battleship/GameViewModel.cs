namespace LinkUpApp.Core.Application.ViewModels.Battleship
{
    public class GameViewModel
    {
        public int GameId { get; set; }
        public string? CurrentUserId { get; set; }
        public string? OpponentId { get; set; }
        public string? OpponentName { get; set; }
        public int Status { get; set; }
        public bool IsMyTurn { get; set; }
        public string? WinnerId { get; set; }
        public bool AmIReady { get; set; }
        public BoardViewModel MyBoard { get; set; } = new();
        public BoardViewModel OpponentBoard { get; set; } = new();
    }
}
