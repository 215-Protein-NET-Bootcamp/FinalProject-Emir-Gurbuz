using Core.Utilities.Messages;

namespace Core.Exceptions.Service
{
    public class NoSelectedMessageResultLanguageException : Exception
    {
        public NoSelectedMessageResultLanguageException() : base(ExceptionMessages.NoSelectedMessageResultLanguage)
        {
        }

        public NoSelectedMessageResultLanguageException(string? message) : base(message)
        {
        }

        public NoSelectedMessageResultLanguageException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
