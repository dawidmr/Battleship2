using Battleship.Game;
using Battleship.Model;

namespace Battleship.Tests
{
    [TestClass]
    public class ShipsVerticalFillerTests
    {
        [DataTestMethod]
        [DataRow(1, 1, 1)]
        [DataRow(4, 1, 4)]
        [DataRow(6, 6, 3)]
        [TestMethod]
        public void Fill_Positive(int gridSize, int shipSize, int shipCount)
        {
            var ships = PrepareShips(new Dictionary<int, int>() { { shipSize, shipCount } });
            var squares = new SquareState[gridSize, gridSize];
            var filler = new ShipsVerticalFiller();

            var shipsOnGrid = filler.Fill(ref squares, gridSize, ships);

            Assert.AreEqual(shipCount, shipsOnGrid.Count, $"There should be {shipCount} ships placed on grid ({gridSize}).");
        }

        [DataTestMethod]
        [DataRow(1, 1, 2)]
        [DataRow(3, 1, 5)]
        [TestMethod]
        [ExpectedException(typeof(FailedToFillGridWithShipsException))]
        public void Fill_Negative(int gridSize, int shipSize, int shipCount)
        {
            var ships = PrepareShips(new Dictionary<int, int>() { { shipSize, shipCount } });
            var filler = new ShipsVerticalFiller();
            var squares = new SquareState[gridSize, gridSize];

            filler.Fill(ref squares, gridSize, ships);
        }

        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(5, 5)]
        [DataRow(10, 6)]
        [TestMethod]
        public void IsPlaceForVerticalShip_Possitive(int gridSize, int shipSize)
        {
            var result = IsPlaceForVerticalShipTest(gridSize, shipSize);

            Assert.IsTrue(result, $"There should be place for ship (size:{shipSize}) on grid (size:{gridSize}).");
        }

        [DataTestMethod]
        [DataRow(1, 2)]
        [DataRow(2, 10)]
        [TestMethod]
        public void IsPlaceForVerticalShip_(int gridSize, int shipSize)
        {
            var result = IsPlaceForVerticalShipTest(gridSize, shipSize);

            Assert.IsFalse(result, $"There should not be place for ship (size:{shipSize}) on grid (size:{gridSize}).");
        }

        [DataTestMethod]
        [DataRow(1, 2, 2, 1)]
        [DataRow(5, 2, 2, 4)]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        [TestMethod]
        public void PlaceShipVertical_Negative(int gridSize, int x, int y, int lenght)
        {
            var filler = new ShipsVerticalFiller();
            var squares = new SquareState[gridSize, gridSize];

            var result = filler.PlaceShipVertical(ref squares, x, y, lenght);

            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(result.First().X == x && result.First().Y == y);
        }

        [DataTestMethod]
        [DataRow(10, 0, 0, 1)]
        [DataRow(10, 9, 9, 1)]
        [TestMethod]
        public void PlaceShipVertical_Positive(int gridSize, int x, int y, int lenght)
        {
            var filler = new ShipsVerticalFiller();
            var squares = new SquareState[gridSize, gridSize];

            var result = filler.PlaceShipVertical(ref squares, x, y, lenght);

            Assert.AreEqual(result.Count, 1);
            Assert.IsTrue(result.First().X == x && result.First().Y == y);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(3, 3)]
        [DataRow(2, 2)]
        [TestMethod]
        public void IsEmptyNeighbourhood_Positive(int x, int y)
        {
            var filler = new ShipsVerticalFiller();
            const int gridSize = 4;
            var squares = new SquareState[gridSize, gridSize];
            FillWithVirgin(squares, gridSize);

            var result = filler.IsEmptyNeighbourhood(squares, gridSize, x, y);

            Assert.IsTrue(result);
        }

        private bool IsPlaceForVerticalShipTest(int gridSize, int shipSize)
        {
            var filler = new ShipsVerticalFiller();
            var squares = new SquareState[gridSize, gridSize];
            return filler.IsPlaceForVerticalShip(squares, gridSize - 1, 0, 0, shipSize);
        }

        private IEnumerable<ShipPrototype> PrepareShips(Dictionary<int, int> shipsSizeCount)
        {
            return shipsSizeCount.Select(s => new ShipPrototype(name: null, size: s.Key, count: s.Value));
        }

        private void FillWithVirgin(SquareState[,] squares, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    squares[i, j] = SquareState.Virgin;
                }
            }
        }
    }
}