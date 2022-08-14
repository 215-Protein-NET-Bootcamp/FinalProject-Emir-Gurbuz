using Core.Dto;
using Core.Entity.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Dto.Concrete
{
    public class OfferReadDto : IReadDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public decimal OfferedPrice { get; set; }
        public byte? Percent { get; set; }
        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
