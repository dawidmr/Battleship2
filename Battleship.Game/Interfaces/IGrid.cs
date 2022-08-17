namespace Battleship.Game;

public interface IGrid
{
    int Size { get; }

    SquareState ChangeSquareState(Coordinates coordinates);
    void Fill(IEnumerable<ShipPrototype> shipPrototypes);
    SquareState[,] GetSquares();
}
