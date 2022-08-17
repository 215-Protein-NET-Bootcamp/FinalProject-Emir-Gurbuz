using Core.Dto;

namespace NTech.Dto.Concrete
{
    public class UserReadDto : IReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
