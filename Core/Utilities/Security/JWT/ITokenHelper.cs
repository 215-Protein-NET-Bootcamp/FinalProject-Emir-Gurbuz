using Core.Entity.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateAccessToken(AppUser appUser, List<IdentityRole> identityRoles);
    }
}
