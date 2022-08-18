using Core.Utilities.Result;
using Moq;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;

namespace NTech.Test.Business
{
    public class OfferManagerTest
    {
        Mock<IOfferService> _offerServiceMock;
        public OfferManagerTest()
        {
            _offerServiceMock = new Mock<IOfferService>();
        }

        [Fact]
        public async Task Offer_add_without_percent_success()
        {
            _offerServiceMock.Setup(x => x.AddAsync(It.IsAny<OfferWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _offerServiceMock.Object.AddAsync(offerWriteDtoWithoutPercent);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Offer_add_without_percent_error()
        {
            _offerServiceMock.Setup(x => x.AddAsync(It.IsAny<OfferWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _offerServiceMock.Object.AddAsync(offerWriteDtoWithoutPercent);

            Assert.False(result.Success);
        }
        [Fact]
        public async Task Offer_add_with_percent_success()
        {
            _offerServiceMock.Setup(x => x.AddAsync(It.IsAny<OfferWriteDto>())).ReturnsAsync(new SuccessResult());

            var result = await _offerServiceMock.Object.AddAsync(offerWriteDtoWithPercent);

            Assert.True(result.Success);
        }
        [Fact]
        public async Task Offer_add_with_percent_error()
        {
            _offerServiceMock.Setup(x => x.AddAsync(It.IsAny<OfferWriteDto>())).ReturnsAsync(new ErrorResult());

            var result = await _offerServiceMock.Object.AddAsync(offerWriteDtoWithPercent);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Offer_accept_success(int offerId)
        {
            _offerServiceMock.Setup(x => x.AcceptOffer(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _offerServiceMock.Object.AcceptOffer(offerId);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Offer_accept_error(int offerId)
        {
            _offerServiceMock.Setup(x => x.AcceptOffer(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _offerServiceMock.Object.AcceptOffer(offerId);

            Assert.False(result.Success);
        }

        [Theory, InlineData(1)]
        public async Task Offer_deny_success(int offerId)
        {
            _offerServiceMock.Setup(x => x.DenyOffer(It.IsAny<int>())).ReturnsAsync(new SuccessResult());

            var result = await _offerServiceMock.Object.DenyOffer(offerId);

            Assert.True(result.Success);
        }
        [Theory, InlineData(1)]
        public async Task Offer_deny_error(int offerId)
        {
            _offerServiceMock.Setup(x => x.DenyOffer(It.IsAny<int>())).ReturnsAsync(new ErrorResult());

            var result = await _offerServiceMock.Object.DenyOffer(offerId);

            Assert.False(result.Success);
        }

        private OfferWriteDto offerWriteDtoWithoutPercent
        {
            get
            {
                return new()
                {
                    ProductId = 1,
                    UserId = 1,
                    OfferedPrice = 150
                };
            }
        }
        private OfferWriteDto offerWriteDtoWithPercent
        {
            get
            {
                return new()
                {
                    ProductId = 1,
                    UserId = 1,
                    Percent = 50
                };
            }
        }
    }
}
