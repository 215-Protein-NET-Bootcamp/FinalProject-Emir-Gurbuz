using Core.Utilities.Result;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IOfferService : IAsyncBaseService<Offer, OfferWriteDto, OfferReadDto>
    {
        Task<IDataResult<List<OfferReadDto>>> GetMyOffersAsync();
        Task<IResult> AcceptOffer(int offerId);
        Task<IResult> DenyOffer(int offerId);
    }
}
