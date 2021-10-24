using System;
using System.Runtime.Serialization;

namespace Exemple.Domain
{
    [Serializable]
    internal class InvalidAdressException : Exception
    {
        public InvalidAdressException()
        {
        }

        public InvalidAdressException(string? message) : base(message)
        {
        }

        public InvalidAdressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidAdressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}