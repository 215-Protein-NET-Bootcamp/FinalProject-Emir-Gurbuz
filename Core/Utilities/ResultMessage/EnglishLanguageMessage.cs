namespace Core.Utilities.ResultMessage
{
    public class EnglishLanguageMessage : ILanguageMessage
    {
        public string SuccessfullyAdded => "Successfully added";

        public string SuccessfullyUpdated => "Successfully updated";

        public string SuccessfullyDeleted => "Successfully deleted";

        public string FailedToAdd => "Failed to add";

        public string FailedToUpdate => "Failed to update";

        public string FailedToDelete => "Failed to delete";

        public string SuccessfullyListed => "Successfully listed";

        public string SuccessfullyGet => "Successfully get";

        public string FailedList => "Failed list";

        public string FailedGet => "Failed get";

        public string NotFound => "Not found";

        public string LoginSuccessfull => "Login successfull";

        public string LoginFailure => "Login failure";

        public string RegisterSuccessfull => "Register successfull";

        public string RegisterFailure => "Register failure";

        public string LockAccount => "Lock account";

        public string UserNotFound => "User not found";

        public string UserAlreadyExists => "User already exists";

        public string OldPasswordWrong => "Old password is wrong";

        public string SuccessResetPassword => "Reset password successful";
        public string FailedResetPassword => "Reset password failed";

        public string OfferedPriceCannotBeHigherThanProductPrice => "Offered price cannot be higher than product price";

        public string OfferIsAlreadyExists => "Offer is already exists";

        public string AcceptOfferSuccess => "Offer is accept successful";

        public string AcceptOfferFailed => "Offer is accept failed";

        public string DenyOfferSuccess => "Offer is denied successful";

        public string DenyOfferFailed => "Offer is denied failed";

        public string ProductHasBeenSold => "Product has been sold";
    }
}
