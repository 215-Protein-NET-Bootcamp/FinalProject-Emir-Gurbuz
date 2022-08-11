using Core.Dto;

namespace NTech.Dto.Concrete
{
    public class ImageReadDto : IReadDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
