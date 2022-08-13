using Battleship.Model;

namespace Battleship.Game
{
    public interface IGrid
    {
        SquareState ChangeSquareState(Coordinates coordinates);
        void Fill(IEnumerable<ShipPrototype> shipPrototypes);
        SquareState[,] GetSquares();
    }
}