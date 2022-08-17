namespace Battleship.Game;

[Serializable]
public class OutOfGridException : Exception
{
    public OutOfGridException(string? message) : base(message)
    {
    }
}
