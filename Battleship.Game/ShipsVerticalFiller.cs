using Battleship.Model;

namespace Battleship.Game
{
    public class ShipsVerticalFiller : IFillStrategy
    {
        protected const int maxAttempts = 100;


        public List<Ship> Fill(ref SquareState[,] squares, int size, IEnumerable<ShipPrototype> shipPrototypes)
        {
            int attemptCount = 0;
            var random = new Random();
            int maxValue = size;
            var ships = new List<Ship>();

            foreach (var ship in shipPrototypes?.OrderByDescending(x => x.size))
            {
                for (int i = 0; i < ship.count; i++)
                {
                    while (attemptCount++ < maxAttempts)
                    {
                        int x = random.Next(maxValue);
                        int y = random.Next(maxValue);

                        bool isPlace = IsPlaceForVerticalShip(squares, maxValue - 1, x, y, ship.size);

                        if (isPlace)
                        {
                            var coordinates = PlaceShipVertical(ref squares, x, y, ship.size);
                            ships.Add(new Ship(coordinates));

                            break;
                        }
                    }

                    if (attemptCount > maxAttempts)
                    {
                        throw new FailedToFillGridWithShipsException($"{nameof(ShipsVerticalFiller)} max attempts: {maxAttempts} reached");
                    }
                    else
                    {
                        attemptCount = 0;
                    }

                }
            }

            return ships;
        }

        public List<Coordinates> PlaceShipVertical(ref SquareState[,] squares, int x, int y, int length)
        {
            var coordinates = new List<Coordinates>();

            for (int i = y; i < y + length; i++)
            {
                squares[x, i] = SquareState.Ship;
                coordinates.Add(new Coordinates((uint)x, (uint)i));
            }

            return coordinates;
        }

        public bool IsPlaceForVerticalShip(SquareState[,] squares, int maxArraySize, int x, int y, int length)
        {
            if (y + length - 1 > maxArraySize)
            {
                return false;
            }

            for (int i = y; i < y + length; i++)
            {
                if (!IsEmptyNeighbourhood(squares, maxArraySize, x, i))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// * * * * *
        /// * n n n *
        /// * n o n *
        /// * n n n *
        /// * * * * *
        /// 
        /// o - point [x,y]
        /// n - Neighbourhood (checked)
        /// * - Not checked
        /// </summary>
        public bool IsEmptyNeighbourhood(SquareState[,] squares, int size, int x, int y)
        {
            for (int checkedY = y - 1; checkedY <= y + 1; checkedY++)
            {
                if (!AreEmpty3SquaresInRow(squares, size, x, checkedY))
                {
                    return false;
                }
            }

            return true;
        }

        public bool AreEmpty3SquaresInRow(SquareState[,] squares, int size, int x, int y)
        {
            if (y < 0 || y > size)
            {
                // row above or below the grid
                return true;
            }

            if (x - 1 >= 0)
            {
               if (squares[x-1, y] != SquareState.Virgin)
                    return false;
            }

            if (squares[x, y] != SquareState.Virgin)
            {
                return false;
            }

            if (x + 1 <= size)
            {
                if (squares[x+1, y] != SquareState.Virgin)
                    return false;
            }

            return true;
        }
    }
}
