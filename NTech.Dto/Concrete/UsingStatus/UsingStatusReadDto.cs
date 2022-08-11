using Core.Dto;

namespace NTech.Dto.Concrete
{
    public class UsingStatusReadDto : IReadDto
    {
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
