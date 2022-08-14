using Core.Dto;

namespace NTech.Dto.Concrete.EmailQueue
{
    public class EmailQueueReadDto : IReadDto
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
