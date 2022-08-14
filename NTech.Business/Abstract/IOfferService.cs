using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.Business.Abstract
{
    public interface IOfferService : IAsyncBaseService<Offer, OfferWriteDto, OfferReadDto>
    {
    }
}
