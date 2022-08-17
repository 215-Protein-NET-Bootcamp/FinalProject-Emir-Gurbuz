using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class BrandsController : BaseController<Brand, BrandWriteDto, BrandReadDto>
    {
        private readonly IBrandService _brandService;
        public BrandsController(IBrandService baseService) : base(baseService)
        {
            _brandService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetListAsync();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Get([FromQuery] BrandFilterResource brandFilterResource)
        {
            var result = await _brandService.GetListByFilterAsync(brandFilterResource);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BrandWriteDto brandWriteDto)
        {
            return await base.AddAsync(brandWriteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] BrandWriteDto brandWriteDto)
        {
            return await base.UpdateAsync(id, brandWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
