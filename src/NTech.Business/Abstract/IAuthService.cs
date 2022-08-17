using Core.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.JWT;

namespace NTech.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto);
        Task<IResult> RegisterAsync(RegisterDto registerDto);
        Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<IDataResult<AccessToken>> CreateAccessToken(User user);
    }
}
