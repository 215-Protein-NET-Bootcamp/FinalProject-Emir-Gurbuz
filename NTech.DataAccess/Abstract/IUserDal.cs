using Core.DataAccess;
using Core.Entity.Concrete;

namespace NTech.DataAccess.Abstract
{
    public interface IUserDal : IAsyncRepository<User>
    {
        Task<List<Role>> GetRolesAsync(int userId);
    }
}
