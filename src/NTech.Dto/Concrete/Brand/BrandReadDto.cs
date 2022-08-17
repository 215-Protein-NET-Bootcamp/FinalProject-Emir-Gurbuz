using Core.Dto;

namespace NTech.Dto.Concrete
{
    public class BrandReadDto : IReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
