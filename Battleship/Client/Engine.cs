namespace Battleship.Client;

public class Engine : IEngine
{
    private readonly IGridCreator _gridCreator;
    private readonly ICoordinatesInterpreter _coordinatesCreator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IEngine> _logger;
    private IGrid _grid;

    public Engine(IGridCreator gridCreator, ICoordinatesInterpreter coordinatesCreator, IConfiguration configuration, ILogger<IEngine> logger)
    {
        _gridCreator = gridCreator;
        _coordinatesCreator = coordinatesCreator;
        _configuration = configuration;
        _logger = logger;
    }

    public SquareState[,] CraeteGrid()
    {
        try
        {
            var config = new BattleshipOptions();
            _configuration.GetSection(BattleshipOptions.Option).Bind(config);

            ValidateConfiguration(config);

            _grid = _gridCreator.CreateGrid(config.GridSize, config.Ships);
            return _grid.GetSquares();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create a Grid");
            throw;
        }
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

    public int GetGridSize() =>
        _grid is null ? 0 : _grid.Size;

    public SquareState[,] Shot(string coordString)
    {
        try
        {
            Coordinates coordinates;

            try
            {
                coordinates = _coordinatesCreator.GetCoordinates(coordString);
            }
            catch
            {
                throw new IncorrectCoordinatesException(coordString);
            }

            _grid.ChangeSquareState(coordinates);
            return _grid.GetSquares();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to Shot with coordinates: {coordString}");
            throw;
        }
    }

    public bool HasGameEnded()
    {
        var squares = _grid.GetSquares();

        foreach (var square in squares)
        {
            if (square == SquareState.Ship)
            {
                return false;
            }
        }

        return true;
    }

}
