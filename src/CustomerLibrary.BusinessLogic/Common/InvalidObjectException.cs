using System;
using System.Runtime.Serialization;

namespace CustomerLibrary.BusinessLogic.Common
{
    public class InvalidObjectException : Exception
    {
        public InvalidObjectException()
        {
        }

        public InvalidObjectException(string message) : base(message)
        {
        }

        public InvalidObjectException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidObjectException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
