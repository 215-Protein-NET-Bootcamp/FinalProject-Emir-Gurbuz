using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;

namespace NTech.Test.Business
{
    public class UsingStatusManagerTest
    {
        Mock<IUsingStatusService> _usingStatusServiceMock;
        public UsingStatusManagerTest()
        {
            _usingStatusServiceMock = new Mock<IUsingStatusService>();
        }

        [Fact]
        public async Task UsingStatus_add_success()
        {
            _usingStatusServiceMock.Setup(x => x.AddAsync(It.IsAny<UsingStatusWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _usingStatusServiceMock.Object.AddAsync(usingStatusWriteDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task UsingStatus_add_error()
        {
            _usingStatusServiceMock.Setup(x => x.AddAsync(It.IsAny<UsingStatusWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _usingStatusServiceMock.Object.AddAsync(usingStatusWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task UsingStatus_update_success(int id)
        {
            _usingStatusServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UsingStatusWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _usingStatusServiceMock.Object.UpdateAsync(id, usingStatusWriteDto);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task UsingStatus_update_error(int id)
        {
            _usingStatusServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<UsingStatusWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _usingStatusServiceMock.Object.UpdateAsync(id, usingStatusWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task UsingStatus_delete_success(int id)
        {
            _usingStatusServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _usingStatusServiceMock.Object.SoftDeleteAsync(id);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task UsingStatus_delete_error(int id)
        {
            _usingStatusServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _usingStatusServiceMock.Object.SoftDeleteAsync(id);

            Assert.False(result.Success);
        }

        private UsingStatusWriteDto usingStatusWriteDto
        {
            get
            {
                return new()
                {
                    Status = "Sıfır"
                };
            }
        }
    }
}
