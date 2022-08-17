using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;

namespace NTech.Test.Business
{
    public class ProductManagerTest
    {
        Mock<IProductService> _productServiceMock;
        public ProductManagerTest()
        {
            _productServiceMock = new Mock<IProductService>();
        }

        [Fact]
        public async Task Product_add_success()
        {
            _productServiceMock.Setup(x => x.AddAsync(It.IsAny<ProductWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _productServiceMock.Object.AddAsync(productWriteDto);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Product_add_error()
        {
            _productServiceMock.Setup(x => x.AddAsync(It.IsAny<ProductWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _productServiceMock.Object.AddAsync(productWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Product_update_success(int id)
        {
            _productServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _productServiceMock.Object.UpdateAsync(id, productWriteDto);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Product_update_error(int id)
        {
            _productServiceMock.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<ProductWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _productServiceMock.Object.UpdateAsync(id, productWriteDto);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Product_delete_success(int id)
        {
            _productServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _productServiceMock.Object.SoftDeleteAsync(id);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Product_delete_error(int id)
        {
            _productServiceMock.Setup(x => x.SoftDeleteAsync(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _productServiceMock.Object.SoftDeleteAsync(id);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Product_Buy_success(int id)
        {
            _productServiceMock.Setup(x => x.BuyAsync(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _productServiceMock.Object.BuyAsync(id);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Product_Buy_error(int id)
        {
            _productServiceMock.Setup(x => x.BuyAsync(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _productServiceMock.Object.BuyAsync(id);

            Assert.False(result.Success);
        }

        private static ProductWriteDto productWriteDto
        {
            get
            {
                return new()
                {
                    Name = "name",
                    Description = "description",
                    Price = 100,
                    isOfferable = false,
                    Rating = 4,
                    BrandId = 1,
                    CategoryId = 1,
                    ColorId = 1,
                    ImageId = 1,
                    UsingStatusId = 1
                };
            }
        }
    }
}
