using System;
using System.Runtime.Serialization;

namespace Exemple.Domain
{
    [Serializable]
    internal class InvalidProdusCantitate : Exception
    {
        public InvalidProdusCantitate()
        {
        }

        public InvalidProdusCantitate(string message) : base(message)
        {
        }

        public InvalidProdusCantitate(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProdusCantitate(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}