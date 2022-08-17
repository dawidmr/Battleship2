using Battleship.Game;
using Battleship.Model;
using Moq;

namespace Battleship.Tests.Game
{
    [TestClass]
    public class GridCreatorTests
    {
        [TestMethod]
        public void CreateGridTest_GridCreatedAndFilled()
        {
            var fillStrategy = new Mock<IFillStrategy>();
            fillStrategy.Setup(fs => fs.Fill(
                ref It.Ref<SquareState[,]>.IsAny, 
                It.IsAny<int>(), 
                It.IsAny<List<ShipPrototype>>()));

            var squareStateTransition = new Mock<ISquareStateTransition>();

            var gridCreator = new GridCreator(fillStrategy.Object, squareStateTransition.Object);

            var gridSize = 10;
            var grid = gridCreator.CreateGrid(gridSize, new List<ShipPrototype>());

            Assert.AreEqual(gridSize, grid.Size);
            fillStrategy.Verify(mock => mock.Fill(ref It.Ref<SquareState[,]>.IsAny,
                It.IsAny<int>(),
                It.IsAny<List<ShipPrototype>>()), Times.Once);

        }
    }
}
