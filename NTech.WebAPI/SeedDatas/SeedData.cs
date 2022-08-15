using Core.Entity.Concrete;
using Core.Utilities.Security.Hashing;

namespace NTech.WebAPI.SeedDatas
{
    public static class SeedData
    {
        public static async Task Seed(IConfiguration configuration)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(
                configuration.GetSection("AdminUser:Password").Value,
                out passwordHash, out passwordSalt
                );
            User user = new User
            {
                FirstName = configuration.GetSection("AdminUser:FirstName").Value,
                LastName = configuration.GetSection("AdminUser:LastName").Value,
                Email = configuration.GetSection("AdminUser:Email").Value,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
        }
    }
}
