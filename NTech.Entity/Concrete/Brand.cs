namespace NTech.Entity.Concrete
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
