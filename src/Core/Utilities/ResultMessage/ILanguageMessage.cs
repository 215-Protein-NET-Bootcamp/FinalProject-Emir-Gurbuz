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

        string FileIsNotNull { get; }
        string UnSupportedFile { get; }
        string FileSizeIsHigh { get; }
        string ProductBuyIsFailed { get; }
        string ProductBuyIsSuccessfully { get; }

        string SuccessfullyFileUpload { get; }
        string FailedToFileUpload { get; }
        string OfferedPriceNotNull { get; }
        string NotSameOldPasswordAndNewPassword { get; }
        string CheckEnteredValues { get; }

        string ColorNotFound { get; }
        string BrandNotFound { get; }
        string UsingStatusNotFound { get; }
        string CategoryNotFound { get; }
        string ImageNotFound { get; }
        string ColorIsAlreadyExists { get; }
        string CategoryIsAlreadyExists { get; }
        string BrandIsAlreadyExists { get; }
        string UsingStatusIsAlreadyExists { get; }
    }
}
