namespace Core.Utilities.ResultMessage
{
    public class TurkishMessageLanguage : ILanguageMessage
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

        public string LoginSuccessfull => "Giriş başarılı";

        public string LoginFailure => "Giriş başarısız";

        public string RegisterSuccessfull => "Kayıt başarılı";

        public string RegisterFailure => "Kayıt başarısız";

        public string LockAccount => "Hesabınız kilitlendi";

        public string UserNotFound => "Kullanıcı bulunamadı";

        public string UserAlreadyExists => "Kullanıcı zaten mevcut";

        public string OldPasswordWrong => "Eski şifre uyuşmuyor";

        public string SuccessResetPassword => "Şifre sıfırlama başarılı";
        public string FailedResetPassword => "Şifre sıfırlama başarısız";

        public string OfferedPriceCannotBeHigherThanProductPrice => "Teklif edilen fiyat ürün fiyatından yüksek olamaz";

        public string OfferIsAlreadyExists => "Teklif zaten verilmiş";

        public string AcceptOfferSuccess => "Teklif başarıyla onaylandı";

        public string AcceptOfferFailed => "Teklif onaylanamadı";

        public string DenyOfferSuccess => "Teklif başarıyla reddedildi";

        public string DenyOfferFailed => "Teklif reddedilemedi";

        public string ProductHasBeenSold => "Satılmış ürüne teklif veremessin";

        public string CannotBeOffer => "Bu ürüne teklif veremessin";

        public string FileIsNotNull => "Dosya boş olamaz";

        public string UnSupportedFile => "Geçersiz dosya uzantısı";

        public string FileSizeIsHigh => "Dosya boyutu yüksek";

        public string ProductBuyIsFailed => "Ürün satın alma başarısız";

        public string ProductBuyIsSuccessfully => "Ürün satın alma başarılı";

        public string SuccessfullyFileUpload => "Dosya yükleme başarılı";

        public string FailedToFileUpload => "Dosya yükleme başarısız";

        public string OfferedPriceNotNull => "Teklif fiyatı boş olamaz";
    }
}
