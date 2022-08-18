using Core.Entity.Concrete;
using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;

namespace NTech.Test.Business
{
    public class EmailQueueManagerTest
    {
        Mock<IEmailQueueService> _emailQueueServiceMock;
        public EmailQueueManagerTest()
        {
            _emailQueueServiceMock = new Mock<IEmailQueueService>();
        }

        [Fact]
        public async Task EmailQueue_add_success()
        {
            _emailQueueServiceMock.Setup(x => x.AddAsync(It.IsAny<EmailQueue>())).ReturnsAsync(new SuccessResult());

            var result = await _emailQueueServiceMock.Object.AddAsync(emailQueue);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task EmailQueue_add_error()
        {
            _emailQueueServiceMock.Setup(x => x.AddAsync(It.IsAny<EmailQueue>())).ReturnsAsync(new ErrorResult());

            var result = await _emailQueueServiceMock.Object.AddAsync(emailQueue);

            Assert.False(result.Success);
        }

        private EmailQueue emailQueue
        {
            get
            {
                return new()
                {
                    Subject = "subject",
                    Body = "body",
                    Email = "username@hotmail.com"
                };
            }
        }
    }
}
