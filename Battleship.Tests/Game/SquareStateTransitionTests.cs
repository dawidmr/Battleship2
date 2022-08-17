using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.Game;
using Battleship.Model;

namespace Battleship.Tests.Game
{
    [TestClass]
    public class SquareStateTransitionTests
    {
        [TestMethod]
        public void GetNewStateTest_IsTransitionForEveryState()
        {
            var squareStateTransition = new SquareStateTransition();

            foreach (SquareState state in Enum.GetValues(typeof(SquareState)))
            {
                Assert.IsNotNull(squareStateTransition.GetNewState(state));
            }
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(SquareState.Virgin, SquareState.MissedShot)]
        [DataRow(SquareState.Virgin, SquareState.HittedShip)]
        [DataRow(SquareState.HittedShip, SquareState.SunkShip)]
        public void IsValidTransitionTest_Positive(SquareState oldState, SquareState newState)
        {
            var squareStateTransition = new SquareStateTransition();
            Assert.IsTrue(squareStateTransition.IsValidTransition(oldState, newState));
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(SquareState.Virgin, SquareState.SunkShip)]
        [DataRow(SquareState.SunkShip, SquareState.HittedShip)]
        [DataRow(SquareState.SunkShip, SquareState.Ship)]
        [DataRow(SquareState.SunkShip, SquareState.Virgin)]
        [DataRow(SquareState.Ship, SquareState.SunkShip)]
        [DataRow(SquareState.Ship, SquareState.MissedShot)]
        [DataRow(SquareState.Ship, SquareState.Virgin)]
        [DataRow(SquareState.MissedShot, SquareState.Virgin)]
        [DataRow(SquareState.MissedShot, SquareState.Ship)]
        [DataRow(SquareState.MissedShot, SquareState.HittedShip)]
        [DataRow(SquareState.MissedShot, SquareState.SunkShip)]
        [DataRow(SquareState.HittedShip, SquareState.Ship)]
        [DataRow(SquareState.HittedShip, SquareState.Virgin)]
        [DataRow(SquareState.HittedShip, SquareState.MissedShot)]
        [DataRow(SquareState.HittedShip, SquareState.Ship)]
        public void IsValidTransitionTest_Negative(SquareState oldState, SquareState newState)
        {
            var squareStateTransition = new SquareStateTransition();
            Assert.IsFalse(squareStateTransition.IsValidTransition(oldState, newState));
        }
    }
}
