using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OffersController : BaseController<Offer, OfferWriteDto, OfferReadDto>
    {
        private readonly IOfferService _offerService;
        public OffersController(IOfferService baseService, IOfferService offerService) : base(baseService)
        {
            _offerService = offerService;
        }

        [HttpGet("myoffers")]
        public async Task<IActionResult> Get()
        {
            var result = await _offerService.GetMyOffersAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
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
        [HttpPut("{id}/accept")]
        public async Task<IActionResult> Accept([FromRoute] int id)
        {
            var result = await _offerService.AcceptOffer(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut("{id}/deny")]
        public async Task<IActionResult> Deny([FromRoute] int id)
        {
            var result = await _offerService.DenyOffer(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
