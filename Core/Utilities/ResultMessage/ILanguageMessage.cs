namespace Core.Utilities.ResultMessage
{
    public interface ILanguageMessage
    {
        string SuccessfullyAdded { get; }
        string SuccessfullyUpdated { get; }
        string SuccessfullyDeleted { get; }

        string FailedToAdd { get; }
        string FailedToUpdate { get; }
        string FailedToDelete { get; }

        string SuccessfullyListed { get; }
        string SuccessfullyGet { get; }

        string FailedList { get; }
        string FailedGet { get; }

        string NotFound { get; }

        string LoginSuccessfull { get; }
        string LoginFailure { get; }

        string RegisterSuccessfull { get; }
        string RegisterFailure { get; }

        string LockAccount { get; }

        string UserNotFound { get; }
        string UserAlreadyExists { get; }

        string OldPasswordWrong { get; }

        string SuccessResetPassword { get; }
        string FailedResetPassword { get; }

        string OfferedPriceCannotBeHigherThanProductPrice { get; }
        string OfferIsAlreadyExists { get; }

        string AcceptOfferSuccess { get; }
        string AcceptOfferFailed { get; }

        string DenyOfferSuccess { get; }
        string DenyOfferFailed { get; }

        string ProductHasBeenSold { get; }
        string CannotBeOffer { get; }
    }
}
