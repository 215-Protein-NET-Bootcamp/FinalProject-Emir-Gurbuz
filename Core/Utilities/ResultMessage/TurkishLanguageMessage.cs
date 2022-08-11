namespace Core.Utilities.ResultMessage
{
    public class TurkishLanguageMessage : ILanguageMessage
    {
        public string SuccessfullyAdded => "Ekleme başarılı";

        public string SuccessfullyUpdated => "Güncelleme başarılı";

        public string SuccessfullyDeleted => "Silme başarılı";

        public string FailedToAdd => "Ekleme başarısız";

        public string FailedToUpdate => "Güncelleme başarısız";

        public string FailedToDelete => "Silme başarısız";

        public string SuccessfullyListed => "Listeleme başarılı";

        public string SuccessfullyGet => "Getirme başarılı";

        public string FailedList => "Listeleme başarısız";

        public string FailedGet => "Getirme başarısız";

        public string NotFound => "Bulunamadı";
    }
}
