namespace Battleship.Game.Interfaces;

public interface ISquareStateTransition
{
    SquareState GetNewState(SquareState currentState);
    bool IsValidTransition(SquareState oldState, SquareState newState);
}
