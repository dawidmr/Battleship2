using System.Runtime.Serialization;

namespace Battleship.Game
{
    [Serializable]
    internal class InvalidStateTransitionException : Exception
    {
        public InvalidStateTransitionException()
        {
        }

        public InvalidStateTransitionException(string? message) : base(message)
        {
        }

        public InvalidStateTransitionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidStateTransitionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}