using Core.Dto;
using System.ComponentModel.DataAnnotations;

namespace NTech.Dto.Concrete
{
    public class BrandWriteDto : IWriteDto
    {
        [Required]
        public string Name { get; set; }
    }
}
