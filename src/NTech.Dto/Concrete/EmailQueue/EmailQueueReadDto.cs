using Core.Dto;

namespace NTech.Dto.Concrete.EmailQueue
{
    public class EmailQueueReadDto : IReadDto
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public byte TryCount { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
