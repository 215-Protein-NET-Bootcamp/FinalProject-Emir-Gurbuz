namespace Core.Entity.Concrete
{
    public class EmailQueue 
    {
        public string Email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public byte TryCount { get; set; }
    }
}
