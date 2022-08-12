using Core.Aspect.Autofac.Validation;
using Core.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Identity;
using NTech.Business.Abstract;
using NTech.Business.Validators.FluentValidation;

namespace NTech.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly ILanguageMessage _languageMessage;
        private readonly IEmailSender _mailService;
        private readonly IMessageBrokerHelper _messageBrokerHelper;

        public AuthManager(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenHelper tokenHelper, ILanguageMessage languageMessage, IEmailSender mailService, IMessageBrokerHelper messageBrokerHelper, IMessageConsumer messageConsumer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHelper = tokenHelper;
            _languageMessage = languageMessage;
            _mailService = mailService;
            _messageBrokerHelper = messageBrokerHelper;
            _messageConsumer = messageConsumer;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(AppUser appUser)
        {
            IList<string> roles = await _userManager.GetRolesAsync(appUser);
            AccessToken accessToken = _tokenHelper.CreateAccessToken(appUser, roles.ToList());

            return new SuccessDataResult<AccessToken>(accessToken, _languageMessage.LoginSuccessfull);
        }
        [ValidationAspect(typeof(LoginDtoValidator))]
        public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);

            if (user.LockoutEnabled)
            {
                if (user.LockoutEnd < DateTime.Now)
                {
                    user.LockoutEnd = null;
                    user.AccessFailedCount = 0;
                    user.LockoutEnabled = false;
                    await _userManager.UpdateAsync(user);
                }
                else
                    return new ErrorDataResult<AccessToken>(_languageMessage.LockAccount);
            }
            else if (user.AccessFailedCount == 3)
            {
                user.LockoutEnd = DateTime.Now.AddMinutes(5);
                user.LockoutEnabled = true;
                await _userManager.UpdateAsync(user);

                await _mailService.SendEmailAsync(new EmailMessage
                {
                    Subject = "Uyarı",
                    Body = $"Sayın {user.FirstName} {user.LastName} üç defa başarısız giriş sonucunda hesabınız kilitlenmiştir. 5 dakika sonra tekrar deneyiniz.",
                    Email = user.Email
                });

                return new ErrorDataResult<AccessToken>(_languageMessage.LockAccount);
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (result.Succeeded)
            {
                _messageBrokerHelper.QueueMessage(user.Email);
                //await _mailService.SendEmailAsync(new EmailMessage
                //{
                //    Subject = "Giriş Başarılı",
                //    Body = $"Hoşgeldiniz {user.FirstName} {user.LastName}",
                //    Email = user.Email
                //});
                var accessToken = await CreateAccessToken(user);
                return accessToken;
            }
            return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
        }
        [ValidationAspect(typeof(RegisterDtoValidator))]
        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            AppUser user = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                LockoutEnabled = false
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.Password);
            if (identityResult.Succeeded == false)
            {
                string errors = string.Join("\n", identityResult.Errors.Select(x => x.Description));
                return new ErrorResult(errors);
            }
            return new SuccessResult(_languageMessage.RegisterSuccessfull);
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

        private async Task sendEmailAsync(string email, string subject, string body)
        {

        }
    }
}
