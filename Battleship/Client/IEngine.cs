using Battleship.Game;
using Battleship.Model;

namespace Battleship.Client
{
    public interface IEngine
    {
        Grid CraeteGrid();
        void Shot(ref Grid grid, Coordinates coordinates);
    }
}