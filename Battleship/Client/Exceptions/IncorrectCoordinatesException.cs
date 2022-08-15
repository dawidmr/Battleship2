namespace Battleship.Client;

[Serializable]
internal class IncorrectCoordinatesException : Exception
{
    public IncorrectCoordinatesException(string? message) : base(message)
    {
    }
}
