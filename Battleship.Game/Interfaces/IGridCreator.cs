namespace Battleship.Game.Interfaces;

public interface IGridCreator
{
    IGrid CreateGrid(int size, IEnumerable<ShipPrototype> ships);
}
