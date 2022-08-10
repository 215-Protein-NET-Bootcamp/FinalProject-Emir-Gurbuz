using Core.Entity;

namespace NTech.Entity.Concrete
{
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public Product Product { get; set; }
    }
}
