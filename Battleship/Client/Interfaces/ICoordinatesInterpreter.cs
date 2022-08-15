namespace Battleship.Client;

public interface ICoordinatesInterpreter
{
    Coordinates GetCoordinates(string value);
}
