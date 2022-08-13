using Battleship.Model;

namespace Battleship.Game
{
    public class GridCreator : IGridCreator
    {
        private readonly IFillStrategy _fillStrategy;
        private readonly ISquareStateTransition _squareStateTransition;

        public GridCreator(IFillStrategy fillStrategy, ISquareStateTransition squareStateTransition)
        {
            _fillStrategy = fillStrategy;
            _squareStateTransition = squareStateTransition;
        }

        public IGrid CreateGrid(int size, IEnumerable<ShipPrototype> ships)
        {
            var grid = new Grid(size, _fillStrategy, _squareStateTransition);
            grid.Fill(ships);

            return grid;
        }
    }
}