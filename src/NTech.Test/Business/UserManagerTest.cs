using Core.Entity.Concrete;
using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;

namespace NTech.Test.Business
{
    public class UserManagerTest
    {
        Mock<IUserService> _userServiceMock;
        public UserManagerTest()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [Fact]
        public async Task User_add_success()
        {
            _userServiceMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(new SuccessResult());

            var result = await _userServiceMock.Object.AddAsync(user);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task User_add_error()
        {
            _userServiceMock.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(new ErrorResult());

            var result = await _userServiceMock.Object.AddAsync(user);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task User_update_success(int id)
        {
            _userServiceMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(new SuccessResult());

            var result = await _userServiceMock.Object.UpdateAsync(user);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task User_update_error(int id)
        {
            _userServiceMock.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(new ErrorResult());

            var result = await _userServiceMock.Object.UpdateAsync(user);

            Assert.False(result.Success);
        }

        [Theory, InlineData(10, 1)]
        public async Task User_add_role_with_id_success(int userId, int roleId)
        {
            _userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _userServiceMock.Object.AddToRoleAsync(userId, roleId);

            Assert.True(result.Success);
        }
        [Theory, InlineData(10, 1)]
        public async Task User_add_role_with_id_error(int userId, int roleId)
        {
            _userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _userServiceMock.Object.AddToRoleAsync(userId, roleId);

            Assert.False(result.Success);
        }

        [Theory, InlineData(10, "User")]
        public async Task User_add_role_with_name_success(int userId, string roleName)
        {
            _userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new SuccessResult());

            var result = await _userServiceMock.Object.AddToRoleAsync(userId, roleName);

            Assert.True(result.Success);
        }
        [Theory, InlineData(10, "User")]
        public async Task User_add_role_with_name_error(int userId, string roleName)
        {
            _userServiceMock.Setup(x => x.AddToRoleAsync(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new ErrorResult());

            var result = await _userServiceMock.Object.AddToRoleAsync(userId, roleName);

            Assert.False(result.Success);
        }


        private User user
        {
            get
            {
                return new()
                {
                    FirstName = "firstName",
                    LastName = "lastName",
                    Email = "username@hotmail.com",
                    DateOfBirth = new DateTime(2002, 9, 8)
                };
            }
        }
    }
}
