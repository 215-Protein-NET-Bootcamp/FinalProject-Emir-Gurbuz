using Core.Entity;
using Core.Entity.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTech.Entity.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int? ColorId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }

        public int? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        public int? UsingStatusId { get; set; }
        [ForeignKey("UsingStatusId")]
        public UsingStatus UsingStatus { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public decimal Price { get; set; }
        public bool isOfferable { get; set; }
        public bool IsSold { get; set; }
        public byte Rating { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
