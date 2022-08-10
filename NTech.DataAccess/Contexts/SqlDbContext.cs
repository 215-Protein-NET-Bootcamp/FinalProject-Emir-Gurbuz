using Microsoft.EntityFrameworkCore;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Contexts
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UsingStatus> UsingStatuses { get; set; }
    }
}
