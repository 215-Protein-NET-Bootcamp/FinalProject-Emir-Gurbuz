using Core.Entity.Concrete;
using Core.Utilities.Result;

namespace NTech.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> UpdateAsync(User user);
        Task<IResult> AddAsync(User user);
        Task<IDataResult<List<User>>> GetListAsync();
        Task<IDataResult<User>> GetByEmailAsync(string email);
        Task<IDataResult<User>> GetByIdAsync(int id);
        Task<List<Role>> GetRolesAsync(int userId);
        Task<IResult> AddToRoleAsync(int userId, int roleId);
        Task<IResult> AddToRoleAsync(int userId, string roleName);
    }
}
