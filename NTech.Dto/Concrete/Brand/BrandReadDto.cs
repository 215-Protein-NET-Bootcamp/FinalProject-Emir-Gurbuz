using Core.Dto;

namespace NTech.Dto.Concrete.Brand
{
    public class BrandReadDto : IReadDto
    {
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
