using Battleship.Client.Game;

namespace Battleship.Client.Interfaces
{
    public interface IGameConfigurator
    {
        BattleshipOptions GetConfig();
    }
}