namespace Battleship.Client.Game;

public class GameConfigurator : IGameConfigurator
{
    private IConfiguration _configuration;

    public GameConfigurator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public BattleshipOptions GetConfig()
    {
        var config = new BattleshipOptions();
        _configuration.GetSection(BattleshipOptions.Option).Bind(config);

        ValidateConfiguration(config);

        return config;
    }

    private void ValidateConfiguration(BattleshipOptions config)
    {
        if (config.GridSize < 1 ||
            config.GridSize > 26 || // letters in alphabet count 
            config.Ships.Count < 1 ||
            config.Ships.Any(ship => ship.Size > config.GridSize))
        {
            throw new WrongConfigurationException();
        }
    }
}
