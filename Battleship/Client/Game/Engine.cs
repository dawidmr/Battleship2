namespace Battleship.Client.Game;

public class Engine : IEngine
{
    private readonly IGridCreator _gridCreator;
    private readonly ICoordinatesInterpreter _coordinatesCreator;
    private readonly IGameConfigurator _configurator;
    private readonly ILogger<IEngine> _logger;
    protected IGrid? _grid;

    private string errorMessage = string.Empty;
    private bool isError = false;

    public Engine(
        IGridCreator gridCreator, 
        ICoordinatesInterpreter coordinatesCreator, 
        IGameConfigurator configurator, 
        ILogger<IEngine> logger)
    {
        _gridCreator = gridCreator;
        _coordinatesCreator = coordinatesCreator;
        _configurator = configurator;
        _logger = logger;
    }

    public SquareState[,]? CraeteGrid()
    {
        try
        {
            var config = _configurator.GetConfig();

            _grid = _gridCreator.CreateGrid(config.GridSize, config.Ships);
            return _grid.GetSquares();
        }
        catch (Exception ex)
        {
            HandleError(ex, "Failed to create a Grid");
            return default;
        }
    }

    public SquareState[,] Shot(string coordString)
    {
        if (_grid == null)
        {
            HandleError(null, "Failed to create a grid");
        }

        try
        {
            Coordinates coordinates;

            try
            {
                coordinates = _coordinatesCreator.GetCoordinates(coordString);
            }
            catch
            {
                return _grid.GetSquares();
            }

            _grid.ChangeSquareState(coordinates);
            return _grid.GetSquares();
        }
        catch (Exception ex)
        {
            HandleError(ex, $"Failed to Shot with coordinates: {coordString}");
            return _grid.GetSquares();
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

    private void HandleError(Exception? ex, string message)
    {
        errorMessage = message;
        isError = true;
        _logger.LogError(ex, errorMessage);
    }
}
