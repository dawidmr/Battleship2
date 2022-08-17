namespace Battleship.Client;

public class ViewFormatting
{
    public static string GetFormat(SquareState state, bool showShips)
    {
        switch (state)
        {
            case SquareState.Ship:
                return showShips ? "bg-primary" : "bg-info";
            case SquareState.HittedShip:
                return "bg-warning";
            case SquareState.SunkShip:
                return "bg-danger";
            case SquareState.Virgin:
                return "bg-info";
            case SquareState.MissedShot:
                return "bg-secondary";
            default:
                return "bg-white";
        }
    }

    public static char GetCharFromSquareState(SquareState state)
    {
        switch (state)
        {
            case SquareState.MissedShot:
                return '*';
            default:
                return ' ';
        }
    }
}
