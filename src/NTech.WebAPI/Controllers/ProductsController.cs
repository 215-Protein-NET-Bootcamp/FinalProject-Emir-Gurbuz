using Core.Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;
using NTech.Dto.Concrete;
using NTech.Entity.Concrete;
using NTech.Entity.Concrete.Filters;

namespace NTech.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : BaseController<Product, ProductWriteDto, ProductReadDto>
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        public ProductsController(IProductService baseService, IImageService imageService) : base(baseService)
        {
            _productService = baseService;
            _imageService = imageService;
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

        [HttpGet("filter")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] ProductFilterResource productFilterResource)
        {
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            var result = await _productService.GetListByFilterAsync(paginationFilter, requestUrl, productFilterResource);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return await base.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductWriteDto productWriteDto)
        {
            var imageResult = await _imageService.UploadAsync(productWriteDto.File);
            if (imageResult.Success == false)
                return BadRequest(imageResult);

            productWriteDto.ImageId = imageResult.Data.Id;
            return await base.AddAsync(productWriteDto);
        }

        [HttpPost("{productId}/upload")]
        public async Task<IActionResult> Post([FromRoute] int productId, IFormFile file)
        {
            var result = await _productService.SetImageAsync(productId, file);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductWriteDto productWriteDto)
        {
            return await base.UpdateAsync(id, productWriteDto);
        }

        [HttpPut("{id}/buy")]
        public async Task<IActionResult> Put([FromRoute] int id)
        {
            var result = await _productService.BuyAsync(id);
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
