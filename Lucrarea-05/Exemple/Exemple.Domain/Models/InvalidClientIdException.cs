using System;
using System.Runtime.Serialization;

namespace Exemple.Domain.Models {
    [Serializable]
    internal class InvalidClientIdException: Exception {
        public InvalidClientIdException() {}

        public InvalidClientIdException(string ? message): base(message) {}

        public InvalidClientIdException(string ? message, Exception ? innerException): base(message, innerException) {}

        protected InvalidClientIdException(SerializationInfo info, StreamingContext context): base(info, context) {}
    }
}