namespace Battleship.Model
{
    public class Ship
    {
        public Ship(List<Coordinates> coordinates)
        {
            Parts = coordinates;
        }

        public List<Coordinates> Parts { get; }
    }
}
