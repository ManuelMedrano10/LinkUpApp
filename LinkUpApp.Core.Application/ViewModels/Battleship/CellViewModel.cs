namespace LinkUpApp.Core.Application.ViewModels.Battleship
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool HasShip { get; set; }
        public bool IsHit { get; set; }

        public string CssClass
        {
            get
            {
                if (IsHit && HasShip) return "bg-danger text-white";
                if (IsHit && !HasShip) return "bg-info bg-opacity-50";
                if (!IsHit && HasShip) return "bg-secondary text-white";
                return "bg-primary bg-opacity-25";
            }
        }
    }
}
