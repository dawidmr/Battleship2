using Battleship.Model;

namespace Battleship.Client
{
    public interface ICoordinatesCreator
    {
        Coordinates GetCoordinates(char column, int row);
    }
}