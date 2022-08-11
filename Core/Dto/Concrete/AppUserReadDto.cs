namespace Core.Dto.Concrete
{
    public class AppUserReadDto : IReadDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}
