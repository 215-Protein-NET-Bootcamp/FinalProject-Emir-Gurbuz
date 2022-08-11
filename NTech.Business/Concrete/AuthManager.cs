using Core.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Identity;
using NTech.Business.Abstract;

namespace NTech.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILanguageMessage _languageMessage;

        public AuthManager(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHelper tokenHelper, ILanguageMessage languageMessage)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _languageMessage = languageMessage;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(AppUser appUser)
        {
            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            AccessToken accessToken = _tokenHelper.CreateAccessToken(appUser, roles.ToList());
            return new SuccessDataResult<AccessToken>(accessToken, _languageMessage.LoginSuccessfull);
        }

        public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (result.Succeeded)
            {
                var accessToken = await CreateAccessToken(user);
                return accessToken;
            }
            return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
        }

        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            AppUser user = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (identityResult.Succeeded)
                return new SuccessResult(_languageMessage.RegisterSuccessfull);
            return new ErrorResult(_languageMessage.RegisterFailure);
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            AppUser user = await _userManager.FindByIdAsync(resetPasswordDto.UserId.ToString());
            if (user == null)
                return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
            IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, "", resetPasswordDto.NewPassword);
            if (identityResult.Succeeded)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
