namespace Battleship.Game;

[Serializable]
internal class UnexpectedSquareStateException : Exception
{
    public UnexpectedSquareStateException(string? message) : base(message)
    {
    }
}
