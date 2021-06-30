using System;
using System.Runtime.Serialization;

namespace CustomerLibrary.BusinessLogic.Common
{
    [Serializable]
    public class NotDeletedException : Exception
    {
        public NotDeletedException()
        {
        }

        public NotDeletedException(string message) : base(message)
        {
        }

        public NotDeletedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotDeletedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
