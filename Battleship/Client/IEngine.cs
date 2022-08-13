namespace Battleship.Client
{
    public interface IEngine
    {
        SquareState[,] CraeteGrid();
        SquareState[,] Shot(string coordinatesString);
    }
}