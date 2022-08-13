namespace Battleship.Client
{
    public class Engine : IEngine
    {
        private readonly IGridCreator _gridCreator;
        private readonly ICoordinatesCreator _coordinatesCreator;
        private IGrid _grid;

        public Engine(IGridCreator gridCreator, ICoordinatesCreator coordinatesCreator)
        {
            _gridCreator = gridCreator;
            _coordinatesCreator = coordinatesCreator;
        }

        public SquareState[,] CraeteGrid()
        {
            _grid = _gridCreator.CreateGrid(10, new List<ShipPrototype> { new ShipPrototype("5", 5, 2) });
            return _grid.GetSquares();
        }

        public SquareState[,] Shot(string coordString)
        {
            Coordinates coordinates;

            try
            {
                var column = coordString[0];
                var row = int.Parse(coordString[1].ToString());

                coordinates = _coordinatesCreator.GetCoordinates(column, row);
                
            }
            catch
            {
                throw new IncorrectCoordinatesException(coordString);
            }

            _grid.ChangeSquareState(coordinates);
            return _grid.GetSquares();
        }

    }
}
