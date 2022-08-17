using Battleship.Client;
using Battleship.Client.Game;
using Battleship.Client.Interfaces;
using Battleship.Game;
using Battleship.Game.Interfaces;
using Battleship.Model;
using Microsoft.Extensions.Logging;
using Moq;

namespace Battleship.Tests.Client;

[TestClass]
public class EngineTests
{
    [TestMethod]
    public void CreateGrid_GridCreatorUsed()
    {
        var gridCreator = new Mock<IGridCreator>();
        gridCreator.Setup(x => x.CreateGrid(It.IsAny<int>(), It.IsAny<IEnumerable<ShipPrototype>>()));

        var interpreter = new Mock<ICoordinatesInterpreter>();
        var gameConfigurator = new Mock<IGameConfigurator>();
        gameConfigurator
            .Setup(x => x.GetConfig())
            .Returns(new BattleshipOptions { GridSize = 10, Ships = new() });

        var logger = new Mock<ILogger<IEngine>>();

        var engine = new Engine(gridCreator.Object, interpreter.Object, gameConfigurator.Object, logger.Object);

        engine.CraeteGrid();

        gridCreator.Verify(x => 
            x.CreateGrid(It.IsAny<int>(), It.IsAny<IEnumerable<ShipPrototype>>()), 
            Times.Once());
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow(SquareState.HittedShip, true)]
    [DataRow(SquareState.Ship, false)]
    public void HasGameEnded_Positive(SquareState squareState, bool hasGameEndedExpected)
    {
        var grid = new Mock<IGrid>();
        grid
            .Setup(x => x.GetSquares())
            .Returns(new SquareState[,] { { squareState } });

        var gridCreator = new Mock<IGridCreator>();
        gridCreator
            .Setup(x => x.CreateGrid(It.IsAny<int>(), It.IsAny<IEnumerable<ShipPrototype>>()))
            .Returns(grid.Object);

        var interpreter = new Mock<ICoordinatesInterpreter>();
        var gameConfigurator = new Mock<IGameConfigurator>();
        gameConfigurator
            .Setup(x => x.GetConfig())
            .Returns(new BattleshipOptions { GridSize = 1, Ships = new() });

        var logger = new Mock<ILogger<IEngine>>();

        var engine = new Engine(gridCreator.Object, interpreter.Object, gameConfigurator.Object, logger.Object);

        engine.CraeteGrid();
        var result = engine.HasGameEnded();

        Assert.AreEqual(hasGameEndedExpected, result);
    }

    [TestMethod]
    [DynamicData(nameof(ShipsToFinalState), DynamicDataSourceType.Method)]
    public void ComponentTest(List<ShipPrototype> ships, SquareState[,] expectedSqareStates)
    {
        var fillStrategy = new ShipsVerticalFiller();
        var transitions = new SquareStateTransition();
        var gridCreator = new GridCreator(fillStrategy, transitions);
        var coordinatesInterpreter = new CoordinatesInterpreter();

        var gameConfigurator = new Mock<IGameConfigurator>();
        gameConfigurator
            .Setup(x => x.GetConfig())
            .Returns(new BattleshipOptions { GridSize = 1, Ships = ships });

        var logger = new Mock<ILogger<IEngine>>();

        var engine = new Engine(gridCreator, coordinatesInterpreter, gameConfigurator.Object, logger.Object);

        engine.CraeteGrid();
        var squareStates = engine.Shot("A1");

        Assert.AreEqual(expectedSqareStates[0, 0], squareStates[0, 0]);
    }

    private static IEnumerable<object[]> ShipsToFinalState() =>
        new List<object[]>
            {
                    new object[] {
                            new List<ShipPrototype> { new ShipPrototype { Size = 1, Count = 1} },
                            new SquareState[,] { { SquareState.SunkShip } } },
                    new object[] {
                            new List<ShipPrototype>(),
                            new SquareState[,] { { SquareState.MissedShot } } }
            };
}
