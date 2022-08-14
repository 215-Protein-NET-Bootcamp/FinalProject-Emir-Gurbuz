using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OffersController : BaseController<Offer, OfferWriteDto, OfferReadDto>
    {
        public OffersController(IAsyncBaseService<Offer, OfferWriteDto, OfferReadDto> baseService) : base(baseService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OfferWriteDto offerWriteDto)
        {
            return await base.AddAsync(offerWriteDto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] OfferWriteDto offerWriteDto)
        {
            return await base.UpdateAsync(id, offerWriteDto);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
