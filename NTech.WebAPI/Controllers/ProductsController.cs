using Core.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController<Product, ProductWriteDto, ProductReadDto>
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService baseService) : base(baseService)
        {
            _productService = baseService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    return await base.GetListAsync();
        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            var result = await _productService.GetPaginationAsync(paginationFilter, requestUrl);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductWriteDto productWriteDto)
        {
            return await base.AddAsync(productWriteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductWriteDto productWriteDto)
        {
            return await base.UpdateAsync(id, productWriteDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return await base.SoftDeleteAsync(id);
        }
    }
}
