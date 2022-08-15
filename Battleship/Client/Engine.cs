namespace Battleship.Client;

public class Engine : IEngine
{
    private readonly IGridCreator _gridCreator;
    private readonly ICoordinatesInterpreter _coordinatesCreator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IEngine> _logger;
    private IGrid _grid;

    private string errorMessage = string.Empty;
    private bool isError = false;

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
            HandleError(ex, "Failed to create a Grid");
            return null;
        }
    }

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
            HandleError(ex, $"Failed to Shot with coordinates: {coordString}");
            return default;
        }
    }

    public (bool, string) GetError()
    {
        return (isError, errorMessage);
    }

    public int GetGridSize() =>
        _grid is null ? 0 : _grid.Size;

    public bool HasGameEnded()
    {
        var squares = _grid?.GetSquares();

        if (squares == null)
        {
            HandleError(ex: null, "Grid is empty");
            return default;
        }

        foreach (var square in squares)
        {
            if (square == SquareState.Ship)
            {
                return false;
            }
        }

        return true;
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

    private void HandleError(Exception ex, string message)
    {
        errorMessage = message;
        isError = true;
        _logger.LogError(ex, errorMessage);
    }
}
