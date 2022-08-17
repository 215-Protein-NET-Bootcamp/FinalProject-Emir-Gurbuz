using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.Business.Extensions
{
    public static class ProductFilterExtensions
    {
        public static IQueryable<Product> getByName(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Name.ToLower().Contains(productFilterResource.Name.ToLower()));
        }
        public static IQueryable<Product> getByDescription(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Description.ToLower().Contains(productFilterResource.Description.ToLower()));
        }
        public static IQueryable<Product> getByMaximumPrice(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Price < productFilterResource.MaximumPrice);
        }
        public static IQueryable<Product> getByMinimumPrice(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.Price > productFilterResource.MinimumPrice);
        }
        public static IQueryable<Product> getByUsingStatusId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.UsingStatusId == productFilterResource.UsingStatusId);
        }
        public static IQueryable<Product> getByColorId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.ColorId == productFilterResource.ColorId);
        }
        public static IQueryable<Product> getByBrandId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.BrandId == productFilterResource.BrandId);
        }
        public static IQueryable<Product> getByCategoryId(this IQueryable<Product> products, ProductFilterResource productFilterResource)
        {
            return products.Where(p => p.CategoryId == productFilterResource.CategoryId);
        }
    }
}
