using Core.Entity.Concrete;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateAccessToken(User user, List<string> roles);
    }
}
