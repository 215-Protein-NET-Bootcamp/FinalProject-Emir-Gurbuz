namespace Core.Entity.Concrete
{
    public class EmailQueue : IEntity
    {
        public EmailQueue()
        {
            TryCount = 0;
        }

        public int Id { get; set; }

        public string Email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public byte TryCount { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
