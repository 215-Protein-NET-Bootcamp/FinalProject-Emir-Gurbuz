using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Extensions
{
    public static class ProductFilterExtensions
    {
        public static IQueryable<Product> GetByName(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Name.ToLower().Contains(productFilterResource.Name.ToLower()));
        }
        public static IQueryable<Product> GetByDescription(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Description.ToLower().Contains(productFilterResource.Description.ToLower()));
        }
        public static IQueryable<Product> GetByMaximumPrice(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Price <= productFilterResource.MaximumPrice);
        }
        public static IQueryable<Product> GetByMinimumPrice(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Price >= productFilterResource.MinimumPrice);
        }
        public static IQueryable<Product> GetByUsingStatusId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.UsingStatusId == productFilterResource.UsingStatusId);
        }
        public static IQueryable<Product> GetByColorId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.ColorId == productFilterResource.ColorId);
        }
        public static IQueryable<Product> GetByBrandId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.BrandId == productFilterResource.BrandId);
        }
        public static IQueryable<Product> GetByCategoryId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.CategoryId == productFilterResource.CategoryId);
        }
    }
}
