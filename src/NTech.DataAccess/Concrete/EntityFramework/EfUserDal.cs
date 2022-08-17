using Core.DataAccess.EntityFramework;
using Core.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NTech.DataAccess.Abstract;

namespace NTech.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfAsyncBaseRepository<User>, IUserDal
    {
        private readonly DbContext _dbContext;
        public EfUserDal(DbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddToRoleAsync(int userId, int roleId)
        {
            EntityEntry entityEntry = await _dbContext.Set<UserRole>().AddAsync(new UserRole { UserId = userId, RoleId = roleId });
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddToRoleAsync(int userId, string roleName)
        {
            Role role = await _dbContext.Set<Role>().FirstOrDefaultAsync(x => x.Name.ToLower() == roleName.ToLower());
            if(role == null)
            {
                return false;
            }
            EntityEntry entityEntry = await _dbContext.Set<UserRole>().AddAsync(new UserRole { UserId = userId, RoleId = role.Id });
            return entityEntry.State == EntityState.Added;
        }

        public async Task<List<Role>> GetRolesAsync(int userId)
        {
            var result = from role in _dbContext.Set<Role>()
                         join userRole in _dbContext.Set<UserRole>()
                         on role.Id equals userRole.RoleId
                         where userRole.UserId == userId
                         select new Role
                         {
                             Id = role.Id,
                             Name = role.Name
                         };
            return await result.ToListAsync();
        }
    }
}
