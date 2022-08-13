using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Model
{
    public record Grid(int Size)
    {
        public SquareStates[,] Squares = new SquareStates[Size, Size];
    }
}
