using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ColorsController : BaseController<Color, ColorWriteDto, ColorReadDto>
    {
        private readonly IColorService _colorService;
        public ColorsController(IColorService baseService) : base(baseService)
        {
            _colorService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetListAsync();
        }

        [NonAction]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ColorFilterResource? colorFilterResouce)
        {
            var result = await _colorService.GetListByFilterAsync(colorFilterResouce);
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
        public async Task<IActionResult> Post([FromBody] ColorWriteDto colorWriteDto)
        {
            return await base.AddAsync(colorWriteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ColorWriteDto colorWriteDto)
        {
            return await base.UpdateAsync(id, colorWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
