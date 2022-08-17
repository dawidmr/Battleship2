using Battleship.Client;
using Battleship.Client.Game;
using Battleship.Model;

namespace Battleship.Tests.Client;

[TestClass]
public class CoordinatesInterpreterTests
{
    [TestMethod]
    [DataTestMethod]
    [DataRow("a")]
    [DataRow("aabc")]
    [DataRow("a")]
    [DataRow("ą12")]
    [DataRow("a123")]
    [DataRow("77")]
    [ExpectedException(typeof(ArgumentException))]
    public void GetCoordinatesTest_Negative(string value)
    {
        var interpreter = new CoordinatesInterpreter();
        interpreter.GetCoordinates(value);
    }

    [TestMethod]
    [DynamicData(nameof(TestData), DynamicDataSourceType.Method)]
    public void GetCoordinatesTest_Positive(string value, Coordinates expectedCoordinates)
    {
        var interpreter = new CoordinatesInterpreter();
        var result = interpreter.GetCoordinates(value);

        Assert.IsTrue(CoordinatesEquals(expectedCoordinates, result));
    }

    private bool CoordinatesEquals(Coordinates c1, Coordinates c2) =>
        c1.X == c2.X && c1.Y == c2.Y;

    private static IEnumerable<object[]> TestData() =>
        new List<object[]> 
            { 
                new object[] { "a1", new Coordinates(0, 0) },
                new object[] { "1a", new Coordinates(0, 0) },
                new object[] { "B2", new Coordinates(1, 1) }
            };
}
