using System;
using System.Runtime.Serialization;

namespace Exemple.Domain
{
    [Serializable]
    internal class InvalidPaymentState : Exception
    {
        public InvalidPaymentState()
        {
        }

        public InvalidPaymentState(string message) : base(message)
        {
        }

        public InvalidPaymentState(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPaymentState(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}