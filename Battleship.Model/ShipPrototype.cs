namespace Battleship.Model
{
    public class ShipPrototype
    {
        public ShipPrototype()
        {
        }

        public ShipPrototype(string name, int size, int count)
        {
            Name = name;
            Size = size;
            Count = count;
        }

        public string Name { get; set; } 
        public int Size { get; set; } 
        public int Count { get; set; }
    }
}
