using Battleship.Game;
using Battleship.Model;
using Moq;

namespace Battleship.Tests.Game;

[TestClass]
public class GridTests
{
    [TestMethod]
    [ExpectedException(typeof(OutOfGridException))]
    public void ChangeSquareStateTest_CoordinatesValidation()
    {
        int gridSize = 5;
        var fillStrategy = new Mock<IFillStrategy>();
        var squareStateTransition = new Mock<ISquareStateTransition>();
        var grid = new Grid(gridSize, fillStrategy.Object, squareStateTransition.Object);

        var coordinates = new Coordinates((uint)gridSize + 1, 1);
        grid.ChangeSquareState(coordinates);
    }

    [TestMethod]
    public void ChangeSquareStateTest_StateChanged()
    {
        int gridSize = 5;
        var testState = SquareState.Ship;
        var fillStrategy = new Mock<IFillStrategy>();
        var squareStateTransition = new Mock<ISquareStateTransition>();
        squareStateTransition
            .Setup(x => x.GetNewState(It.IsAny<SquareState>()))
            .Returns(testState);

        var grid = new Grid(gridSize, fillStrategy.Object, squareStateTransition.Object);

        var coordinates = new Coordinates(1, 1);
        var resultState = grid.ChangeSquareState(coordinates);

        Assert.AreEqual(testState, resultState);
    }

    [TestMethod]
    public void ChangeSquareStateTest_SunkShip()
    {
        int gridSize = 1;
        var testState = SquareState.HittedShip;
        var ships = new List<Ship> { new ( new() { new Coordinates(0, 0) }) };

        var fillStrategy = new Mock<IFillStrategy>();
        fillStrategy
            .Setup(fs => fs.Fill(
                ref It.Ref<SquareState[,]>.IsAny,
                It.IsAny<int>(),
                It.IsAny<List<ShipPrototype>>()))
            .Returns(ships);

        var squareStateTransition = new Mock<ISquareStateTransition>();
        squareStateTransition
            .Setup(x => x.GetNewState(It.IsAny<SquareState>()))
            .Returns(testState);

        squareStateTransition
        .Setup(x => x.IsValidTransition(It.IsAny<SquareState>(), It.IsAny<SquareState>()))
        .Returns(true);

        var grid = new Grid(gridSize, fillStrategy.Object, squareStateTransition.Object);
        grid.Fill(new List<ShipPrototype>());

        var coordinates = new Coordinates(0, 0);
        var resultState = grid.ChangeSquareState(coordinates);

        Assert.AreEqual(SquareState.SunkShip, resultState);
    }
}
