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
    }
}
