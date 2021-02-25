using System;
using System.Runtime.Serialization;

namespace APIServer
{
    [Serializable]
    internal class AlreadyListeningException : Exception
    {
        public AlreadyListeningException()
        {
        }

        public AlreadyListeningException(string message) : base(message)
        {
        }

        public AlreadyListeningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyListeningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}