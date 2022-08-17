using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UsingStatusesController : BaseController<UsingStatus, UsingStatusWriteDto, UsingStatusReadDto>
    {
        private readonly IUsingStatusService _usingStatusService;
        public UsingStatusesController(IUsingStatusService baseService) : base(baseService)
        {
            _usingStatusService = baseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetListAsync();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Get([FromQuery] UsingStatusFilterResource usingStatusFilterResource)
        {
            var result = await _usingStatusService.GetListByFilterAsync(usingStatusFilterResource);
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
        public async Task<IActionResult> Post([FromBody] UsingStatusWriteDto usingStatusWriteDto)
        {
            return await base.AddAsync(usingStatusWriteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UsingStatusWriteDto usingStatusWriteDto)
        {
            return await base.UpdateAsync(id, usingStatusWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
