using Battleship.Model;

namespace Battleship.Client
{
    public class ViewFormatting
    {
        public static string GetFormat(SquareStates state)
        {
            switch (state)
            {
                case SquareStates.Ship:
                    return "bg-primary";
                case SquareStates.HittedShip:
                    return "bg-warning";
                case SquareStates.SunkShip:
                    return "bg-danger";
                case SquareStates.Virgin:
                    return "bg-info";
                case SquareStates.MissedShot:
                    return "bg-secondary";
                default:
                    return "bg-white";
            }
        }

        public static char GetCharFromSquareState(SquareStates state)
        {
            switch (state)
            {
                case SquareStates.Ship:
                case SquareStates.HittedShip:
                    return 'H';
                case SquareStates.Virgin:
                    return 'V';
                case SquareStates.SunkShip:
                    return 'X';
                case SquareStates.MissedShot:
                    return '*';
                default:
                    return '?';
            }
        }
    }
}
