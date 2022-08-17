using Core.Utilities.Messages;
using System.Runtime.Serialization;

namespace Core.Exceptions.Extension
{
    public class NotFoundUserIdException : Exception
    {
        public NotFoundUserIdException() : base(ExceptionMessages.UserIdNotFound)
        {
        }

        public NotFoundUserIdException(string? message) : base(message)
        {
        }

        protected NotFoundUserIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static void ThrowIfNull(object? obj)
        {
            if (obj == null)
                throw new NotFoundUserIdException();
        }
    }
}
