using Battleship.Model;

namespace Battleship.Game
{
    public class Grid : IGrid
    {
        public int Size { get; }
        protected SquareStates[,] Squares;
        private ISquareStateTransition _squareStateTransitions;
        private IFillStrategy _fillStrategy;
        private List<Ship> ships;

        public SquareStates[,] GetSquares() => Squares;

        public Grid(int size, IFillStrategy fillStrategy, ISquareStateTransition squareStateTransitions)
        {
            Size = size;
            Squares = new SquareStates[size, size];
            _fillStrategy = fillStrategy;
            _squareStateTransitions = squareStateTransitions;
        }

        public void Fill(IEnumerable<ShipPrototype> shipPrototypes)
        {
            ships = _fillStrategy.Fill(ref Squares, Size, shipPrototypes);
        }

        public SquareStates ChangeSquareState(Coordinates coordinates, SquareStates? suggestedState)
        {
            ValidateCoordinates(coordinates);

            var currentState = Squares[coordinates.X, coordinates.Y];
            SquareStates newState;

            if (suggestedState.HasValue)
            {
                newState = _squareStateTransitions.GetNewState(currentState, suggestedState.Value);
                Squares[coordinates.X, coordinates.Y] = newState;
            }
            else
            {
                newState = _squareStateTransitions.GetNewState(currentState);
                Squares[coordinates.X, coordinates.Y] = newState;

                if (newState == SquareStates.HittedShip &&
                    IsSunkShip(coordinates))
                {
                    ValidateTransition(newState, SquareStates.SunkShip);
                    newState = SquareStates.SunkShip;
                    SetStateForWholeShip(coordinates, newState);
                }                
            }

            return newState;
        }

        public bool IsAnyVirginSquare()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Squares[i, j] == SquareStates.Virgin)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void ValidateTransition(SquareStates oldState, SquareStates newState)
        {
            if (_squareStateTransitions.IsValidTransition(oldState, newState) == false)
            {
                throw new InvalidStateTransitionException($"Old state: {oldState}, new state: {newState}");
            }
        }

        private void ValidateCoordinates(Coordinates coordinates)
        {
            if (coordinates.X > Size || coordinates.Y > Size)
            {
                throw new OutOfGridException($"X: {coordinates.X}, Y: {coordinates.Y}, size: {Size}.");
            }
        }

        private void SetStateForWholeShip(Coordinates coordinates, SquareStates newState)
        {
            var ship = GetShip(coordinates);

            ship.Parts.ForEach(p => Squares[p.X, p.Y] = newState);
        }

        private Ship GetShip(Coordinates coords)
        {
            return ships.FirstOrDefault(s => s.Parts.Any(p => p.X == coords.X && p.Y == coords.Y));
        }

        private bool IsSunkShip(Coordinates coordinates)
        {
            var ship = GetShip(coordinates);

            return ship?.Parts.All(c =>
                Squares[c.X, c.Y] == SquareStates.HittedShip ||
                Squares[c.X, c.Y] == SquareStates.SunkShip) ??
                false;
        }
    }
}
