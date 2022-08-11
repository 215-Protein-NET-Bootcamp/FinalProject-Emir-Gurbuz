using Core.Dto;
using NTech.Entity.Concrete;

namespace NTech.Dto.Concrete
{
    public class ProductReadDto : IReadDto
    {
        public CategoryReadDto Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ColorReadDto Color { get; set; }
        public BrandReadDto Brand { get; set; }
        public UsingStatusReadDto UsingStatus { get; set; }
        public ImageReadDto Image { get; set; }
        public decimal Price { get; set; }
        public bool isOfferable { get; set; }
        public byte Rating { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
