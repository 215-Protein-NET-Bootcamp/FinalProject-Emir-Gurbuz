using Core.Entity;
using Core.Entity.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTech.Entity.Concrete
{
    public class Offer : IEntity
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public decimal? OfferedPrice { get; set; }
        public byte? Percent { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
