using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController<Category, CategoryWriteDto, CategoryReadDto>
    {
        public CategoriesController(ICategoryService baseService) : base(baseService)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryWriteDto categoryWriteDto)
        {
            return await base.AddAsync(categoryWriteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CategoryWriteDto categoryWriteDto)
        {
            return await base.UpdateAsync(id, categoryWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
