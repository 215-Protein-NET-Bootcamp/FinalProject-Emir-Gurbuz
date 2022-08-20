using Core.Dto;
using System.ComponentModel.DataAnnotations;

namespace NTech.Dto.Concrete
{
    public class UsingStatusWriteDto : IWriteDto
    {
        [Required]
        public string Status { get; set; }
    }
}
