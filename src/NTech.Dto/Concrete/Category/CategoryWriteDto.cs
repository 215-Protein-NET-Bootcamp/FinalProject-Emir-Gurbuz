using Core.Dto;
using System.ComponentModel.DataAnnotations;

namespace NTech.Dto.Concrete
{
    public class CategoryWriteDto : IWriteDto
    {
        [Required]
        public string Name { get; set; }
    }
}
