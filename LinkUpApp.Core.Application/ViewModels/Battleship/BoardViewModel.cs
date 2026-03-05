namespace LinkUpApp.Core.Application.ViewModels.Battleship
{
    public class BoardViewModel
    {
        public CellViewModel[,] Grid { get; set; } = new CellViewModel[12, 12];

        public BoardViewModel()
        {
            for (int r = 0; r < 12; r++)
            {
                for (int c = 0; c < 12; c++)
                {
                    Grid[r, c] = new CellViewModel { Row = r, Col = c };
                }
            }
        }
    }
}
