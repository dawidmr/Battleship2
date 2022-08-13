namespace Battleship.Client
{
    public interface IEngine
    {
        SquareState[,] CraeteGrid();
        bool HasGameEnded();
        SquareState[,] Shot(string coordinatesString);
    }
}