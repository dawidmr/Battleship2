using Battleship.Model;

namespace Battleship.Game
{
    public interface IGrid
    {
        void Fill(IEnumerable<ShipPrototype> shipPrototypes);
    }
}