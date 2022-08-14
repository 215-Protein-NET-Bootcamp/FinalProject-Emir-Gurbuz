using Core.Dto;

namespace NTech.Dto.Concrete.EmailQueue
{
    public class EmailQueueWriteDto : IWriteDto
    {
        string Data { get; set; }
    }
}
