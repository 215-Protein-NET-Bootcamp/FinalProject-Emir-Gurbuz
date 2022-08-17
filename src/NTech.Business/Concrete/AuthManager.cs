using Core.Aspect.Autofac.Validation;
using Core.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Business;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using NTech.Business.Abstract;
using NTech.Business.Validators.FluentValidation;

namespace NTech.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly ILanguageMessage _languageMessage;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        private readonly IUserService _userService;

        public AuthManager(ITokenHelper tokenHelper, ILanguageMessage languageMessage, IMessageBrokerHelper messageBrokerHelper, IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _languageMessage = languageMessage;
            _messageBrokerHelper = messageBrokerHelper;
            _userService = userService;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User appUser)
        {
            List<Role> roles = await _userService.GetRolesAsync(appUser.Id);
            AccessToken accessToken = _tokenHelper.CreateAccessToken(appUser, roles.Select(x => x.Name).ToList());

            return new SuccessDataResult<AccessToken>(accessToken, _languageMessage.LoginSuccessfull);
        }
        [ValidationAspect(typeof(LoginDtoValidator))]
        public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto)
        {
            var userResult = await _userService.GetByEmailAsync(loginDto.Email);
            if (userResult.Success == false)
                return new ErrorDataResult<AccessToken>(_languageMessage.UserNotFound);
            User user = userResult.Data;

            if (user.LockoutEnd > DateTime.Now)
            {
                user.AccessFailedCount = 0;
                await _userService.UpdateAsync(user);
            }
            if (user.AccessFailedCount >= 3)
            {
                EmailQueue emailQueue = new()
                {
                    Subject = "Uyarı",
                    Body = $"Sayın {user.FirstName} {user.LastName} üç defa başarısız giriş sonucunda hesabınız kilitlenmiştir. 3 dakika sonra tekrar deneyiniz.",
                    Email = user.Email
                };
                user.LockoutEnd = DateTime.Now.AddMinutes(3);
                await _userService.UpdateAsync(user);
                _messageBrokerHelper.QueueMessage(emailQueue);
                return new ErrorDataResult<AccessToken>(_languageMessage.LockAccount);
            }

            if (HashingHelper.VerifyPasswordHash(loginDto.Password,
                user.PasswordHash,
                user.PasswordSalt) == false)
            {
                user.AccessFailedCount++;
                await _userService.UpdateAsync(user);
                return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
            }
            IDataResult<AccessToken> accessToken = await CreateAccessToken(user);

            if (accessToken.Success)
            {
                EmailQueue emailQueue = new()
                {
                    Subject = "Giriş Başarılı",
                    Body = $"Hoşgeldiniz {user.FirstName} {user.LastName}",
                    Email = user.Email
                };
                user.AccessFailedCount = 0;
                user.LockoutEnd = null;
                await _userService.UpdateAsync(user);
                _messageBrokerHelper.QueueMessage(emailQueue);
                return accessToken;
            }

            return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
        }
        [ValidationAspect(typeof(RegisterDtoValidator))]
        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            var findUserResult = await _userService.GetByEmailAsync(registerDto.Email);
            if (findUserResult.Success == true)
                return new ErrorDataResult<AccessToken>(_languageMessage.UserAlreadyExists);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            User user = new()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                DateOfBirth = registerDto.DateOfBirth,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            var result = await _userService.AddAsync(user);
            return result.Success ?
                new SuccessResult(_languageMessage.RegisterSuccessfull) :
                new ErrorResult(_languageMessage.RegisterFailure);
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var userResult = await _userService.GetByIdAsync(resetPasswordDto.UserId);
            if (userResult.Success == false)
                return new ErrorDataResult<AccessToken>(_languageMessage.UserNotFound);

            var result = BusinessRule.Run(
                checkOldPassword(userResult.Data, resetPasswordDto.OldPassword));
            if (result != null)
                return result;

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(resetPasswordDto.NewPassword, out passwordHash, out passwordSalt);

            userResult.Data.PasswordHash = passwordHash;
            userResult.Data.PasswordSalt = passwordSalt;

            IResult updateResult = await _userService.UpdateAsync(userResult.Data);
            //int row = await _unitOfWork.CompleteAsync();
            return updateResult.Success == true ?
                new SuccessResult(_languageMessage.SuccessResetPassword) :
                new ErrorResult(_languageMessage.FailedResetPassword);
        }

        private IResult checkOldPassword(User user, string oldPassword)
        {
            if (HashingHelper.VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt) == false)
            {
                return new ErrorResult(_languageMessage.OldPasswordWrong);
            }
            return new SuccessResult();
        }
    }
}
