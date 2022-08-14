using Core.Dto;

namespace NTech.Dto.Concrete
{
    public class OfferReadDto : IReadDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public ProductReadDto Product { get; set; }
        public UserReadDto User { get; set; }
        public decimal OfferedPrice { get; set; }
        public byte? Percent { get; set; }
        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
