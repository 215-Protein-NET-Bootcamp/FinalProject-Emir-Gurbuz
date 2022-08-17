using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;

namespace NTech.Test.Business
{
    public class BrandManagerTest
    {
        Mock<IBrandService> _brandServiceMock;
        public BrandManagerTest()
        {
            _brandServiceMock = new Mock<IBrandService>();
        }

        [Fact]
        public async Task Brand_add_success()
        {
            _brandServiceMock.Setup(x => x.AddAsync(It.IsAny<BrandWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _brandServiceMock.Object.AddAsync(brandWriteDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Brand_add_error()
        {
            _brandServiceMock.Setup(x => x.AddAsync(It.IsAny<BrandWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _brandServiceMock.Object.AddAsync(brandWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Brand_update_success(int id)
        {
            _brandServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _brandServiceMock.Object.UpdateAsync(id, brandWriteDto);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Brand_update_error(int id)
        {
            _brandServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<BrandWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _brandServiceMock.Object.UpdateAsync(id, brandWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Brand_delete_success(int id)
        {
            _brandServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _brandServiceMock.Object.SoftDeleteAsync(id);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Brand_delete_error(int id)
        {
            _brandServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _brandServiceMock.Object.SoftDeleteAsync(id);

            Assert.False(result.Success);
        }

        private BrandWriteDto brandWriteDto
        {
            get
            {
                return new()
                {
                    Name = "Apple"
                };
            }
        }
    }
}
