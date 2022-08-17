using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;

namespace NTech.Test.Business
{
    public class CategoryManagerTest
    {
        Mock<ICategoryService> _categoryServiceMock;
        public CategoryManagerTest()
        {
            _categoryServiceMock = new Mock<ICategoryService>();
        }

        [Fact]
        public async Task Category_add_success()
        {
            _categoryServiceMock.Setup(x => x.AddAsync(It.IsAny<CategoryWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _categoryServiceMock.Object.AddAsync(categoryWriteDto);

            Assert.True(result.Success);

        }
        [Fact]
        public async Task Category_add_error()
        {
            _categoryServiceMock.Setup(x => x.AddAsync(It.IsAny<CategoryWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _categoryServiceMock.Object.AddAsync(categoryWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Category_update_success(int id)
        {
            _categoryServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _categoryServiceMock.Object.UpdateAsync(id, categoryWriteDto);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Category_update_error(int id)
        {
            _categoryServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<CategoryWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _categoryServiceMock.Object.UpdateAsync(id, categoryWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Category_delete_success(int id)
        {
            _categoryServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _categoryServiceMock.Object.SoftDeleteAsync(id);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Category_delete_error(int id)
        {
            _categoryServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _categoryServiceMock.Object.SoftDeleteAsync(id);

            Assert.False(result.Success);
        }


        private CategoryWriteDto categoryWriteDto
        {
            get
            {
                return new()
                {
                    Name = "Bilgisayar"
                };
            }
        }
    }
}
