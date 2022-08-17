using Core.Dto.Concrete;
using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.JWT;
using Moq;
using NTech.Business.Abstract;

namespace NTech.Test.Business
{
    public class AuthManagerTest
    {
        Mock<IAuthService> _authServiceMock;
        public AuthManagerTest()
        {
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task Create_access_token_success()
        {
            _authServiceMock.Setup(x => x.CreateAccessToken(It.IsAny<User>())).ReturnsAsync(new SuccessDataResult<AccessToken>());

            var result = await _authServiceMock.Object.CreateAccessToken(user);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Create_access_token_error()
        {
            _authServiceMock.Setup(x => x.CreateAccessToken(It.IsAny<User>())).ReturnsAsync(new ErrorDataResult<AccessToken>());

            var result = await _authServiceMock.Object.CreateAccessToken(user);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_success()
        {
            _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync(new SuccessDataResult<AccessToken>());

            var result = await _authServiceMock.Object.LoginAsync(loginDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Login_error()
        {
            _authServiceMock.Setup(x => x.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync(new ErrorDataResult<AccessToken>());

            var result = await _authServiceMock.Object.LoginAsync(loginDto);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Register_success()
        {
            _authServiceMock.Setup(x => x.RegisterAsync(It.IsAny<RegisterDto>())).ReturnsAsync(new SuccessDataResult<AccessToken>());

            var result = await _authServiceMock.Object.RegisterAsync(registerDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Register_error()
        {
            _authServiceMock.Setup(x => x.RegisterAsync(It.IsAny<RegisterDto>())).ReturnsAsync(new ErrorDataResult<AccessToken>());

            var result = await _authServiceMock.Object.RegisterAsync(registerDto);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Reset_password_success()
        {
            _authServiceMock.Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordDto>())).ReturnsAsync(new SuccessResult());

            var result = await _authServiceMock.Object.ResetPasswordAsync(resetPasswordDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Reset_password_error()
        {
            _authServiceMock.Setup(x => x.ResetPasswordAsync(It.IsAny<ResetPasswordDto>())).ReturnsAsync(new ErrorResult());

            var result = await _authServiceMock.Object.ResetPasswordAsync(resetPasswordDto);

            Assert.False(result.Success);
        }

        private LoginDto loginDto
        {
            get
            {
                return new()
                {
                    Email = "username@hotmail.com",
                    Password = "123456Aa"
                };
            }
        }
        private RegisterDto registerDto
        {
            get
            {
                return new()
                {
                    FirstName = "Emir",
                    LastName = "Gürbüz",
                    Email = "username@hotmail.com",
                    Password = "123456Aa",
                    DateOfBirth = new DateTime(2002, 9, 8)
                };
            }
        }
        private User user
        {
            get
            {
                return new()
                {
                    Id = 1,
                    FirstName = "Emir",
                    LastName = "Gürbüz",
                    Email = "username@hotmail.com",
                    PasswordHash = new byte[1],
                    PasswordSalt = new byte[1],
                    Status = true,
                    DateOfBirth = new DateTime(2002, 9, 8)
                };
            }
        }
        private ResetPasswordDto resetPasswordDto
        {
            get
            {
                return new()
                {
                    NewPassword = "1234567Aa",
                    OldPassword = "123456Aa",
                    UserId = 1
                };
            }
        }
    }
}
