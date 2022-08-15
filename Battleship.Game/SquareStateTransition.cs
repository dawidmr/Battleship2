namespace Battleship.Game;

public class SquareStateTransition : ISquareStateTransition
{
    public SquareState GetNewState(SquareState currentState) => currentState switch
    {
        SquareState.HittedShip => SquareState.HittedShip,
        SquareState.MissedShot => SquareState.MissedShot,
        SquareState.SunkShip => SquareState.SunkShip,
        SquareState.Virgin => SquareState.MissedShot,
        SquareState.Ship => SquareState.HittedShip,
        _ => throw new UnexpectedSquareStateException($"{nameof(SquareStateTransition)}: {nameof(currentState)})")
    };

    public bool IsValidTransition(SquareState oldState, SquareState newState)
    {
        return (oldState == SquareState.Virgin && newState == SquareState.MissedShot ||
            oldState == SquareState.Virgin && newState == SquareState.HittedShip ||
            oldState == SquareState.HittedShip && newState == SquareState.SunkShip);
    }
}
