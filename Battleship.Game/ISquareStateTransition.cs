using Battleship.Model;

namespace Battleship.Game
{
    public interface ISquareStateTransition
    {
        SquareStates GetNewState(SquareStates currentState);
        SquareStates GetNewState(SquareStates currentState, SquareStates suggestedState);
        bool IsValidTransition(SquareStates oldState, SquareStates newState);
    }
}