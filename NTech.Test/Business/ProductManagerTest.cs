using Moq;
using NTech.Business.Abstract;
using NTech.Business.Concrete;

namespace NTech.Test.Business
{
    public class ProductManagerTest
    {
        Mock<IProductService> _productService;
        public ProductManagerTest()
        {

        }

        [Fact]
        public void Test()
        {
            _productService.Object.BuyAsync(1);
        }
    }
}
