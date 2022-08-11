using Core.Dto;

namespace NTech.Dto.Concrete.Color
{
    public class ColorReadDto : IReadDto
    {
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
