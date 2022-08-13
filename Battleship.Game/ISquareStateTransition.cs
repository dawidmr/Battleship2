using Battleship.Model;

namespace Battleship.Game
{
    public interface ISquareStateTransition
    {
        SquareState GetNewState(SquareState currentState);
        bool IsValidTransition(SquareState oldState, SquareState newState);
    }
}