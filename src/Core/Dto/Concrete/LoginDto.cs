namespace Core.Dto.Concrete
{
    public class LoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
