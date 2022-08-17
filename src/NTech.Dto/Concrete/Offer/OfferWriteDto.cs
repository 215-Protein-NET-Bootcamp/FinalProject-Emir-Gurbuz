using Core.Dto;

namespace NTech.Dto.Concrete
{
    public class OfferWriteDto : IWriteDto
    {
        public int ProductId { get; set; }
        public int? UserId { get; set; }
        public decimal? OfferedPrice { get; set; }
        public int? Percent { get; set; }
        public bool? Status { get; set; }
    }
}
