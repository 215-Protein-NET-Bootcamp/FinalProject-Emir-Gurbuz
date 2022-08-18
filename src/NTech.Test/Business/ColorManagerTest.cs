using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;

namespace NTech.Test.Business
{
    public class ColorManagerTest
    {
        Mock<IColorService> _colorServiceMock;
        public ColorManagerTest()
        {
            _colorServiceMock = new Mock<IColorService>();
        }

        [Fact]
        public async Task Color_add_success()
        {
            _colorServiceMock.Setup(x => x.AddAsync(It.IsAny<ColorWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _colorServiceMock.Object.AddAsync(colorWriteDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Color_add_error()
        {
            _colorServiceMock.Setup(x => x.AddAsync(It.IsAny<ColorWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _colorServiceMock.Object.AddAsync(colorWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Color_update_success(int id)
        {
            _colorServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<ColorWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _colorServiceMock.Object.UpdateAsync(id, colorWriteDto);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Color_update_error(int id)
        {
            _colorServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<ColorWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _colorServiceMock.Object.UpdateAsync(id, colorWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Color_delete_success(int id)
        {
            _colorServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _colorServiceMock.Object.SoftDeleteAsync(id);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Color_delete_error(int id)
        {
            _colorServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _colorServiceMock.Object.SoftDeleteAsync(id);

            Assert.False(result.Success);
        }

        private ColorWriteDto colorWriteDto
        {
            get
            {
                return new()
                {
                    Name = "Siyah"
                };
            }
        }
    }
}
