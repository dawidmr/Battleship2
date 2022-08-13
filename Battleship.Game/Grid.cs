using Battleship.Model;

namespace Battleship.Game
{
    public class Grid : IGrid
    {
        public int Size { get; }
        protected SquareState[,] Squares;
        private ISquareStateTransition _squareStateTransitions;
        private IFillStrategy _fillStrategy;
        private List<Ship> ships;

        public SquareState[,] GetSquares() => Squares;

        public Grid(int size, IFillStrategy fillStrategy, ISquareStateTransition squareStateTransitions)
        {
            Size = size;
            Squares = new SquareState[size, size];
            _fillStrategy = fillStrategy;
            _squareStateTransitions = squareStateTransitions;
        }

        public void Fill(IEnumerable<ShipPrototype> shipPrototypes)
        {
            ships = _fillStrategy.Fill(ref Squares, Size, shipPrototypes);
        }

        public SquareState ChangeSquareState(Coordinates coordinates)
        {
            ValidateCoordinates(coordinates);

            var currentState = Squares[coordinates.X, coordinates.Y];
            SquareState newState;

            newState = _squareStateTransitions.GetNewState(currentState);
            Squares[coordinates.X, coordinates.Y] = newState;

            if (newState == SquareState.HittedShip &&
                IsPartSunkShip(coordinates))
            {
                ValidateTransition(newState, SquareState.SunkShip);
                newState = SquareState.SunkShip;
                SetStateForWholeShip(coordinates, newState);
            }
            return newState;
        }

        private void ValidateTransition(SquareState oldState, SquareState newState)
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

        private void SetStateForWholeShip(Coordinates coordinates, SquareState newState)
        {
            var ship = GetShip(coordinates);

            ship.Parts.ForEach(part => ValidateTransition(GetCoordinatesState(part), newState));
            ship.Parts.ForEach(p => Squares[p.X, p.Y] = newState);
        }

        private Ship GetShip(Coordinates coords) =>
            ships.FirstOrDefault(s => 
                s.Parts.Any(p => 
                    p.X == coords.X && 
                    p.Y == coords.Y));

        private bool IsPartSunkShip(Coordinates coordinates)
        {
            var ship = GetShip(coordinates);

            return ship?.Parts.All(c =>
                GetCoordinatesState(c) == SquareState.HittedShip ||
                GetCoordinatesState(c) == SquareState.SunkShip) ??
                false;
        }

        private SquareState GetCoordinatesState(Coordinates coordinates) =>
            Squares[coordinates.X, coordinates.Y];

    }
}
