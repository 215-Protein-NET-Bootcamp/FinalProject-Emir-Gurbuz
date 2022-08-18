using Core.Utilities.Messages;
using Core.Utilities.ResultMessage;

namespace Core.Exceptions.Extension
{
    public class WrongLanguageMessageTypeException : Exception
    {
        public WrongLanguageMessageTypeException() : base(ExceptionMessages.NullLanguageMessage)
        {
        }

        public WrongLanguageMessageTypeException(string? message) : base(message)
        {
        }

        public WrongLanguageMessageTypeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public static void ThrowIfNotAssignable(object argument1)
        {
            if (typeof(ILanguageMessage).IsAssignableFrom(argument1.GetType()) == false)
                throw new WrongLanguageMessageTypeException();
        }
        public static void ThrowIfNotAssignable(Type argument1)
        {
            if (typeof(ILanguageMessage).IsAssignableFrom(argument1) == false)
                throw new WrongLanguageMessageTypeException();
        }
    }
}
