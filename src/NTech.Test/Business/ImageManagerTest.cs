using Core.Utilities.Result;
using Microsoft.AspNetCore.Http;
using Moq;
using NTech.Business.Abstract;
using NTech.Entity.Concrete;

namespace NTech.Test.Business
{
    public class ImageManagerTest
    {
        Mock<IImageService> _imageServiceMock;
        Mock<IFormFile> _formFileMock;
        public ImageManagerTest()
        {
            _imageServiceMock = new Mock<IImageService>();
            _formFileMock = new Mock<IFormFile>();
        }

        [Fact]
        public async Task Upload_image_success()
        {
            _imageServiceMock.Setup(x => x.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new SuccessDataResult<Image>());

            var result = await _imageServiceMock.Object.UploadAsync(_formFileMock.Object);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Upload_image_error()
        {
            _imageServiceMock.Setup(x => x.UploadAsync(It.IsAny<IFormFile>())).ReturnsAsync(new ErrorDataResult<Image>());

            var result = await _imageServiceMock.Object.UploadAsync(_formFileMock.Object);

            Assert.False(result.Success);
        }
    }
}
