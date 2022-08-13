namespace Battleship.Game
{
    internal class FailedToFillGridWithShipsException : Exception
    {
        public FailedToFillGridWithShipsException(string? message) : base(message)
        {
        }
    }
}