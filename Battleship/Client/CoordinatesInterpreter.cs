namespace Battleship.Client;

public class CoordinatesInterpreter : ICoordinatesInterpreter
{
    public Coordinates GetCoordinates(string value)
    {
        if (value.Length is < 2 or > 3)
        {
            throw new ArgumentException(GetErrorMessage(value));
        }

        var firstLetter = value.First();
        var lastLetter = value.Last();
        char column = char.MinValue;
        string row = string.Empty;

        if (IsColumnLetter(firstLetter))
        {
            column = firstLetter;
            row = value.Substring(1);
        }
        else if (IsColumnLetter(lastLetter))
        {
            column = lastLetter;
            row = value.Substring(0, value.Length - 1);
        }
        else
        {
            throw new ArgumentException(GetErrorMessage(value));
        }

        try
        {
            return new(GetColumnNumber(column), GetRowNumber(row));
        }
        catch
        {
            throw new ArgumentException(GetErrorMessage(value));
        }
    }

    private string GetErrorMessage(string value) =>
        $"{value} is not a proper coordinate.";


    private uint GetColumnNumber(char column) => column switch
    {
        >= 'A' and <= 'J' => (uint)column - 'A',
        >= 'a' and <= 'j' => (uint)column - 'a',
        _ => throw new ArgumentException(column.ToString())
    };

    private bool IsColumnLetter(char letter) =>
        letter is (>= 'A' and <= 'J') or (>= 'a' and <= 'j');


    private uint GetRowNumber(string row) =>
        uint.Parse(row) - 1;
}