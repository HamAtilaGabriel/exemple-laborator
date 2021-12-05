using System;
using System.Runtime.Serialization;

namespace Exemple.Domain.Models {
    [Serializable]
    internal class InvalidShoppingCartEntryException: Exception {
        public InvalidShoppingCartEntryException() {}

        public InvalidShoppingCartEntryException(string ? message): base(message) {}

        public InvalidShoppingCartEntryException(string ? message, Exception ? innerException): base(message, innerException) {}

        protected InvalidShoppingCartEntryException(SerializationInfo info, StreamingContext context): base(info, context) {}
    }
}