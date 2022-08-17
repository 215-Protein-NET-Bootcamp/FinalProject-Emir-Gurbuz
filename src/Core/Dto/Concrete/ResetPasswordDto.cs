namespace Core.Dto.Concrete
{
    public class ResetPasswordDto : IDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
