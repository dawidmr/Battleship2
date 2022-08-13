using Battleship.Model;

namespace Battleship.Game
{
    public class StateTransition : ISquareStateTransition
    {
        public SquareStates GetNewState(SquareStates currentState)
        {
            return UpdateState(currentState);
        }

        public SquareStates GetNewState(SquareStates currentState, SquareStates suggestedState)
        {
            return UpdateState(new Tuple<SquareStates, SquareStates>(currentState, suggestedState));
        }

        private static SquareStates UpdateState(Tuple<SquareStates, SquareStates> states) => states switch
        {
            { Item1: SquareStates.Virgin, Item2: SquareStates.Virgin } => SquareStates.MissedShot,
            { Item1: SquareStates.Virgin, Item2: SquareStates.HittedShip } => SquareStates.HittedShip,
            { Item1: SquareStates.Virgin, Item2: SquareStates.SunkShip } => SquareStates.SunkShip,
            _ => throw new InvalidStateTransitionException($"{nameof(StateTransition)} Old state: {states.Item1}, new state: {states.Item2}")
        };

        private static SquareStates UpdateState(SquareStates currentState) => currentState switch
        {
            SquareStates.HittedShip => SquareStates.HittedShip,
            SquareStates.MissedShot => SquareStates.MissedShot,
            SquareStates.SunkShip => SquareStates.SunkShip,
            SquareStates.Virgin => SquareStates.MissedShot,
            _ => throw new UnexpectedSquareStateException($"{nameof(StateTransition)}: {nameof(currentState)})")
        };

        public bool IsValidTransition(SquareStates oldState, SquareStates newState)
        {
            return (oldState == SquareStates.Virgin && newState == SquareStates.MissedShot ||
                oldState == SquareStates.Virgin && newState == SquareStates.HittedShip ||
                oldState == SquareStates.HittedShip && newState == SquareStates.SunkShip);
        }
    }
}
