namespace Battleship.Game;

[Serializable]
internal class OutOfGridException : Exception
{
    public OutOfGridException(string? message) : base(message)
    {
    }
}
