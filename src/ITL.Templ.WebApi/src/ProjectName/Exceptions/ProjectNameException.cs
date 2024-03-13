using System;
using System.Runtime.Serialization;

namespace ProjectName.Exceptions
{
    [Serializable]
    public class ProjectNameException : Exception
    {
        public ProjectNameException()
        {
        }

        public ProjectNameException(string message)
            : base(message)
        {
        }

        public ProjectNameException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProjectNameException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
