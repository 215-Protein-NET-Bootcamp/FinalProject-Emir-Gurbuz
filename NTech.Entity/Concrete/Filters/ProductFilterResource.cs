namespace NTech.Entity.Concrete.Filters
{
    public class ProductFilterResource
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? MaximumPrice { get; set; }
        public decimal? MinimumPrice { get; set; }
        public int? UsingStatusId { get; set; }
        public int? BrandId { get; set; }
        public int? ColorId { get; set; }
        public int? CategoryId { get; set; }
        public byte? MinimumRating { get; set; }
        public byte? MaximumRating { get; set; }
    }
}
