using Core.Dto;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;

namespace NTech.WebAPI.Controllers
{
    [ApiController]
    public class BaseController<TEntity, TDto> : ControllerBase
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {
        private readonly IAsyncBaseService<TEntity, TDto> BaseService;

        public BaseController(IAsyncBaseService<TEntity, TDto> baseService)
        {
            BaseService = baseService;
        }
        [NonAction]
        public async Task<IActionResult> AddAsync(TDto dto)
        {
            var result = await BaseService.AddAsync(dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [NonAction]
        public async Task<IActionResult> UpdateAsync(int id, TDto dto)
        {
            var result = await BaseService.UpdateAsync(id, dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [NonAction]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await BaseService.DeleteAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [NonAction]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await BaseService.GetByIdAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [NonAction]
        public async Task<IActionResult> GetListAsync()
        {
            var result = await BaseService.GetListAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
