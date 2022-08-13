namespace Battleship.Client
{
    public class CoordinatesCreator : ICoordinatesCreator
    {
        public Coordinates GetCoordinates(char column, int row) =>
            new(GetColumnNumber(column), GetRowNumber(row));

        private uint GetColumnNumber(char column) => column switch
        {
            >= 'A' and <= 'J' => (uint)column - 'A',
            >= 'a' and <= 'j' => (uint)column - 'a',
            _ => throw new UnexpectedColumnException(column)
        };

        private uint GetRowNumber(int row) =>
            (uint)row - 1;

    }
}
