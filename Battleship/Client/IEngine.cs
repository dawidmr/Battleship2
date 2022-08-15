namespace Battleship.Client;

public interface IEngine
{
    SquareState[,] CraeteGrid();
    int GetGridSize();
    bool HasGameEnded();
    SquareState[,] Shot(string coordinatesString);
}
