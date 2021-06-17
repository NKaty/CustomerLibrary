using System;
using System.Runtime.Serialization;

namespace CustomerLibrary.BusinessLogic.Common
{
    [Serializable]
    public class NotCreatedException : Exception
    {
        public NotCreatedException()
        {
        }

        public NotCreatedException(string message) : base(message)
        {
        }

        public NotCreatedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotCreatedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}