using Core.Utilities.Messages;

namespace Core.Exceptions.Service
{
    public class NoSelectedDatabaseException : Exception
    {
        public NoSelectedDatabaseException() : base(ExceptionMessages.NoSelectedDatabase)
        {
        }

        public NoSelectedDatabaseException(string? message) : base(message)
        {
        }

        public NoSelectedDatabaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
