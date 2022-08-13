using Battleship.Model;

namespace Battleship.Game
{
    public interface IFillStrategy
    {
        List<Ship> Fill(ref SquareStates[,] squares, int size, IEnumerable<ShipPrototype> shipPrototypes);
    }
}
