using Core.Entity;
using Core.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using NTech.Core.Extensions;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Contexts
{
    public class NTechDbContext : DbContext
    {
        public NTechDbContext(DbContextOptions options) : base(options)
        {

        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IEntity>();
            entries.SetStateDate();
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UsingStatus> UsingStatuses { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
