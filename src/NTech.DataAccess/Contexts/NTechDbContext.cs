using Core.Entity;
using Core.Entity.Concrete;
using Core.Utilities.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NTech.Core.Extensions;
using NTech.Entity.Concrete;

namespace NTech.DataAccess.Contexts
{
    public class NTechDbContext : DbContext
    {
        public NTechDbContext(DbContextOptions options) : base(options)
        {
            #region PostgreSql EnableLegacyTimestampBehavior
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            #endregion
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<IEntity>();
            entries.SetStateDate();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            addRoles(modelBuilder);
            addAdminUser(modelBuilder);
            addUserRoles(modelBuilder);

            addCategories(modelBuilder);
            addBrands(modelBuilder);
            addColors(modelBuilder);
            addUsingStatuses(modelBuilder);
        }
        private void addRoles(ModelBuilder modelBuilder)
        {
            Role[] roleEntitySeeds = {
                new() { Id = 1, Name = "User", CreatedDate = DateTime.Now },
                new() { Id = 2, Name = "Admin",CreatedDate = DateTime.Now }
            };

            modelBuilder.Entity<Role>().HasData(roleEntitySeeds);
        }
        private void addAdminUser(ModelBuilder modelBuilder)
        {
            User user = getAdminUser();

            User[] userEntitySeeds = {
                user
            };

            modelBuilder.Entity<User>().HasData(userEntitySeeds);
        }
        private void addUserRoles(ModelBuilder modelBuilder)
        {
            UserRole[] userRoleEntitySeeds = {
                new() { Id = 1, UserId = 1, RoleId = 1 } ,
                new() { Id = 2, UserId = 1, RoleId = 2}
            };

            modelBuilder.Entity<UserRole>().HasData(userRoleEntitySeeds);
        }
        private void addCategories(ModelBuilder modelBuilder)
        {
            Category[] categoryEntitySeeds =
            {
                new() {Id=1,Name="Telefon",CreatedDate=DateTime.Now},
                new() {Id=2,Name="Akıllı Saat",CreatedDate=DateTime.Now}
            };

            modelBuilder.Entity<Category>().HasData(categoryEntitySeeds);
        }
        private void addBrands(ModelBuilder modelBuilder)
        {
            Brand[] brandEntitySeeds =
            {
                new() {Id=1,Name="Samsung",CreatedDate=DateTime.Now},
                new() {Id=2,Name="Xiaomi",CreatedDate=DateTime.Now}
            };

            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);
        }
        private void addUsingStatuses(ModelBuilder modelBuilder)
        {
            UsingStatus[] usingStatusEntitySeeds =
            {
                new() {Id=1,Status="Sıfır",CreatedDate=DateTime.Now},
                new() {Id=2,Status="İkinci El",CreatedDate=DateTime.Now},
                new() {Id=3,Status="Az Kullanılmış",CreatedDate=DateTime.Now}
            };

            modelBuilder.Entity<UsingStatus>().HasData(usingStatusEntitySeeds);
        }
        private void addColors(ModelBuilder modelBuilder)
        {
            Color[] colorEntitySeeds =
            {
                new() {Id=1,Name="Siyah",CreatedDate=DateTime.Now},
                new() {Id=2,Name="Beyaz",CreatedDate=DateTime.Now},
                new() {Id=3,Name="Mor",CreatedDate=DateTime.Now},
                new() {Id=4,Name="Pembe",CreatedDate=DateTime.Now},
                new() {Id=5,Name="Mavi",CreatedDate=DateTime.Now},
                new() {Id=6,Name="Yeşil",CreatedDate=DateTime.Now},
                new() {Id=7,Name="Lacivert",CreatedDate=DateTime.Now},
                new() {Id=8,Name="Kırmızı",CreatedDate=DateTime.Now},
                new() {Id=9,Name="Gri",CreatedDate=DateTime.Now},
                new() {Id=10,Name="Sarı",CreatedDate=DateTime.Now}
            };

            modelBuilder.Entity<Color>().HasData(colorEntitySeeds);
        }

        private User getAdminUser()
        {
            User user = Configuration.GetSection("AdminUser").Get<User>();

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(Configuration.GetSection("AdminUser:Password").Value, out passwordHash, out passwordSalt);

            user.Id = 1;
            user.DateOfBirth = DateTime.Now;
            user.LockoutEnabled = false;
            user.Status = true;
            user.CreatedDate = DateTime.Now;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return user;
        }

        private IConfigurationRoot Configuration
        {
            get
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                var build = configurationBuilder.Build();
                return build;
            }
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
