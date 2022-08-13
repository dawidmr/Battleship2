using Battleship.Model;

namespace Battleship.Game
{
    public interface IGridCreator
    {
        Grid CreateGrid(int size, IEnumerable<ShipPrototype> ships);
    }
}