using System.Runtime.Serialization;

namespace Battleship.Client
{
    [Serializable]
    internal class UnexpectedColumnException : Exception
    {
        private char _column;

        public UnexpectedColumnException()
        {
        }

        public UnexpectedColumnException(char column)
        {
            _column = column;
        }

        public UnexpectedColumnException(string? message) : base(message)
        {
        }

        public UnexpectedColumnException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnexpectedColumnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}