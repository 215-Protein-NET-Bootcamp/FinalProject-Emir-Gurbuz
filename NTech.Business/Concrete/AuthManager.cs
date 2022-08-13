using Core.Aspect.Autofac.Performance;
using Core.Aspect.Autofac.Validation;
using Core.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Mail;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using NTech.Business.Abstract;
using NTech.Business.Validators.FluentValidation;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;

namespace NTech.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly ILanguageMessage _languageMessage;
        private readonly IUserDal _userDal;
        private readonly IMessageBrokerHelper _messageBrokerHelper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthManager(ITokenHelper tokenHelper, ILanguageMessage languageMessage, IMessageBrokerHelper messageBrokerHelper, IUserDal userDal, IUnitOfWork unitOfWork)
        {
            _tokenHelper = tokenHelper;
            _languageMessage = languageMessage;
            _messageBrokerHelper = messageBrokerHelper;
            _userDal = userDal;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<AccessToken>> CreateAccessToken(User appUser)
        {
            List<Role> roles = await _userDal.GetRolesAsync(appUser.Id);
            AccessToken accessToken = _tokenHelper.CreateAccessToken(appUser, roles.Select(x => x.Name).ToList());

            return new SuccessDataResult<AccessToken>(accessToken, _languageMessage.LoginSuccessfull);
        }
        [ValidationAspect(typeof(LoginDtoValidator))]
        public async Task<IDataResult<AccessToken>> LoginAsync(LoginDto loginDto)
        {
            User user = await _userDal.GetAsync(x => x.Email.ToLower() == loginDto.Email.ToLower());
            if (user == null)
                return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);

            if (user.AccessFailedCount >= 3)
            {
                EmailQueue emailQueue = new()
                {
                    Subject = "Uyarı",
                    Body = $"Sayın {user.FirstName} {user.LastName} üç defa başarısız giriş sonucunda hesabınız kilitlenmiştir. 3 dakika sonra tekrar deneyiniz.",
                    Email = user.Email,
                    TryCount = 0
                };
                _messageBrokerHelper.QueueMessage(emailQueue);
                return new ErrorDataResult<AccessToken>(_languageMessage.LockAccount);
            }

            if (user.LockoutEnd > DateTime.Now)
            {
                user.AccessFailedCount = 0;
                await _userDal.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();
            }

            if (HashingHelper.VerifyPasswordHash(loginDto.Password,
                user.PasswordHash,
                user.PasswordSalt) == false)
            {
                user.AccessFailedCount++;
                await _userDal.UpdateAsync(user);
                await _unitOfWork.CompleteAsync();
                return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
            }
            var accessToken = await CreateAccessToken(user);

            if (accessToken.Success)
            {
                EmailQueue emailQueue = new()
                {
                    Subject = "Giriş Başarılı",
                    Body = $"Hoşgeldiniz {user.FirstName} {user.LastName}",
                    Email = user.Email,
                    TryCount = 0
                };
                _messageBrokerHelper.QueueMessage(emailQueue);
                return accessToken;
            }

            return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
        }
        [ValidationAspect(typeof(RegisterDtoValidator))]
        public async Task<IResult> RegisterAsync(RegisterDto registerDto)
        {
            User findUser = await _userDal.GetAsync(x => x.Email.ToLower() == registerDto.Email.ToLower());
            if (findUser != null)
                return new ErrorDataResult<AccessToken>(_languageMessage.RegisterFailure);

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
            await _userDal.AddAsync(user);
            int row = await _unitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(_languageMessage.RegisterSuccessfull) :
                new ErrorResult(_languageMessage.RegisterFailure);
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            //User user = await _userManager.FindByIdAsync(resetPasswordDto.UserId.ToString());
            //if (user == null)
            //    return new ErrorDataResult<AccessToken>(_languageMessage.LoginFailure);
            //IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, "", resetPasswordDto.NewPassword);
            //if (identityResult.Succeeded)
            //{
            //    return new SuccessResult();
            //}
            //return new ErrorResult();
            return null;
        }

        private async Task sendEmailAsync(string email, string subject, string body)
        {

        }
    }
}
