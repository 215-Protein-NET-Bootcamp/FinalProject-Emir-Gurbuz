using Core.Utilities.Messages;
using System.Runtime.Serialization;

namespace Core.Exceptions.Log
{
    public class SerilogNotFoundFolderPathException : Exception
    {
        public SerilogNotFoundFolderPathException() : base(ExceptionMessages.SerilogNotFoundFolderPath)
        {
        }

        public SerilogNotFoundFolderPathException(string? message) : base(message)
        {
        }

        protected SerilogNotFoundFolderPathException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
