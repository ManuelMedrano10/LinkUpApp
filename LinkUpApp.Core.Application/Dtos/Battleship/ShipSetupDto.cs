namespace LinkUpApp.Core.Application.Dtos.Battleship
{
    public class ShipSetupDto
    {
        public int Size { get; set; }
        public bool IsHorizontal { get; set; }
        public int StartRow { get; set; }
        public int StartCol { get; set; }
    }
}
