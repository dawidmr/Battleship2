namespace Battleship.Game;

public interface IGridCreator
{
    IGrid CreateGrid(int size, IEnumerable<ShipPrototype> ships);
}
