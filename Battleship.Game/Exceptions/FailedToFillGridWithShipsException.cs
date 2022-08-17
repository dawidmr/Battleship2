namespace Battleship.Game;

public class FailedToFillGridWithShipsException : Exception
{
    public FailedToFillGridWithShipsException(string? message) : base(message)
    {
    }
}
