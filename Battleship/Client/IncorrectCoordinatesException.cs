using System.Runtime.Serialization;

namespace Battleship.Client
{
    [Serializable]
    internal class IncorrectCoordinatesException : Exception
    {
        public IncorrectCoordinatesException()
        {
        }

        public IncorrectCoordinatesException(string? message) : base(message)
        {
        }

        public IncorrectCoordinatesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IncorrectCoordinatesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}