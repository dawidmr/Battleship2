namespace Battleship.Client
{
    public class BattleshipOptions
    {
        public const string Option = "Battleship";

        public int Size { get; set; } = 0;

        public List<ShipPrototype> Ships { get; set; }

    }
}
