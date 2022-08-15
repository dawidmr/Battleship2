namespace Battleship.Game;

[Serializable]
internal class InvalidStateTransitionException : Exception
{
    public InvalidStateTransitionException(string? message) : base(message)
    {
    }
}
