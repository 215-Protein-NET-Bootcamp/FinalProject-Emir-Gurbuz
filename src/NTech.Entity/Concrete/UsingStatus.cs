using Core.Entity;

namespace NTech.Entity.Concrete
{
    public class UsingStatus : IEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
