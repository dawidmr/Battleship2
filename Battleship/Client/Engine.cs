using Battleship.Game;
using Battleship.Model;

namespace Battleship.Client
{
    public class Engine : IEngine
    {
        private readonly IGridCreator _gridCreator;

        public Engine(IGridCreator gridCreator)
        {
            _gridCreator = gridCreator;
        }

        public Grid CraeteGrid()
        {
            return _gridCreator.CreateGrid(10, new List<ShipPrototype> { new ShipPrototype("5", 5, 2) });
        }

        public void Shot(ref Grid grid, Coordinates coordinates)
        {

        }
    }
}
