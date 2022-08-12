using Core.Utilities.Messages;

namespace Core.Exceptions.Aspect
{
    public class WrongLoggingTypeException : Exception
    {
        public WrongLoggingTypeException() : base(ExceptionMessages.WrongLoggingType)
        {
        }

        public WrongLoggingTypeException(string? message) : base(message)
        {
        }

        public WrongLoggingTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNotEqual(Type argument1, Type argument2)
        {
            if (argument1.IsAssignableFrom(argument2) == false)
                throw new WrongLoggingTypeException();
        }
    }
}
