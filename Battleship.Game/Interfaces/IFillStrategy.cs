namespace Battleship.Game.Interfaces;

public interface IFillStrategy
{
    List<Ship> Fill(ref SquareState[,] squares, int size, IEnumerable<ShipPrototype> shipPrototypes);
}
