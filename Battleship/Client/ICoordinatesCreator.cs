using Battleship.Model;

namespace Battleship.Client
{
    public interface ICoordinatesCreator
    {
        Coordinates GetCoordinates(string value);
    }
}