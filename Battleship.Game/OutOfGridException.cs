using System.Runtime.Serialization;

namespace Battleship.Game
{
    [Serializable]
    internal class OutOfGridException : Exception
    {
        public OutOfGridException()
        {
        }

        public OutOfGridException(string? message) : base(message)
        {
        }

        public OutOfGridException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OutOfGridException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}