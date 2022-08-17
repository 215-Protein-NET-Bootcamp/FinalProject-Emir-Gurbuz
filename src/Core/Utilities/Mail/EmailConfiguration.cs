namespace Core.Utilities.Mail
{
    public class EmailConfiguration
    {
        public int Port { get; set; }
        public string Server { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
