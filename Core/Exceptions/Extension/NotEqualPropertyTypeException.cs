using Core.Utilities.Messages;

namespace Core.Exceptions.Extension
{
    public class NotEqualPropertyTypeException : Exception
    {
        public NotEqualPropertyTypeException() : base(ExceptionMessages.NotEqualPropertyType)
        {
        }

        public NotEqualPropertyTypeException(string? message) : base(message)
        {
        }

        public NotEqualPropertyTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
