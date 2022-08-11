using Core.Dto;
using NTech.Entity.Concrete;

namespace NTech.Dto.Concrete
{
    public class ProductReadDto : IReadDto
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Color Color { get; set; }
        public Brand Brand { get; set; }
        public UsingStatus UsingStatus { get; set; }
        public Image Image { get; set; }
        public decimal Price { get; set; }
        public bool isOfferable { get; set; }
        public byte Rating { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
