using Core.Utilities.Messages;

namespace Core.Exceptions.Aspect
{
    public class WrongValidationTypeException : Exception
    {
        public WrongValidationTypeException() : base(ExceptionMessages.WrongValidationType)
        {
        }

        public WrongValidationTypeException(string? message) : base(message)
        {
        }

        public WrongValidationTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNotEqual(Type argument1, Type argument2)
        {
            if (argument1 != argument2)
                throw new WrongValidationTypeException();
        }
    }
}
