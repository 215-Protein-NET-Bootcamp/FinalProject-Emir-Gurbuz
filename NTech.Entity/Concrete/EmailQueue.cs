using Core.Entity;

namespace NTech.Entity.Concrete
{
    public class EmailQueue : IEntity
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
