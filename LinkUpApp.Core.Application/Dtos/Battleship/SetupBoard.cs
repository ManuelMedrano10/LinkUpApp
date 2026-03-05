namespace LinkUpApp.Core.Application.Dtos.Battleship
{
    public class SetupBoard
    {
        public class SetupBoardDto
        {
            public int GameId { get; set; }
            public string? PlayerId { get; set; }
            public List<ShipSetupDto> Ships { get; set; } = [];
        }
    }
}
