using Core.Dto;
using Microsoft.AspNetCore.Http;

namespace NTech.Dto.Concrete
{
    public class ProductWriteDto : IWriteDto
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ColorId { get; set; }
        public int? BrandId { get; set; }
        public int UsingStatusId { get; set; }
        public int? ImageId { get; set; }
        public decimal Price { get; set; }
        public bool? isOfferable { get; set; }
        public byte Rating { get; set; }

        public IFormFile File { get; set; }
    }
}
