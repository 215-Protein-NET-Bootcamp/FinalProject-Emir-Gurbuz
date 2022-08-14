using Core.Utilities.Messages;

namespace Core.Exceptions.Middleware
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base(ExceptionMessages.Unauthorized)
        {
        }

        public UnauthorizedException(string? message) : base(message)
        {
        }

        public UnauthorizedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
