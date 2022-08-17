namespace Battleship.Client;

public interface IEngine
{
    SquareState[,]? CraeteGrid();
    (bool, string) GetError();
    int GetGridSize();
    bool HasGameEnded();
    SquareState[,] Shot(string coordinatesString);
}
