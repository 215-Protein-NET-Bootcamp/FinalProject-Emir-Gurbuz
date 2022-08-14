using Core.DataAccess;
using Core.Entity.Concrete;

namespace NTech.DataAccess.Abstract
{
    public interface IUserDal : IAsyncRepository<User>
    {
        Task<List<Role>> GetRolesAsync(int userId);
        Task<bool> AddToRoleAsync(int userId, int roleId);
        Task<bool> AddToRoleAsync(int userId, string roleName);
    }
}
