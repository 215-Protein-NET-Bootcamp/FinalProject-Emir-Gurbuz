using Core.Dto;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using NTech.Business.Abstract;

namespace NTech.WebAPI.Controllers
{
    [ApiController]
    public class BaseController<TEntity, TWriteDto, TReadDto> : ControllerBase
        where TEntity : class, IEntity, new()
        where TWriteDto : class, IWriteDto, new()
        where TReadDto : class, IReadDto, new()
    {
        protected readonly IAsyncBaseService<TEntity, TWriteDto, TReadDto> BaseService;

        public BaseController(IAsyncBaseService<TEntity, TWriteDto, TReadDto> baseService)
        {
            BaseService = baseService;
        }
        [NonAction]
        public async Task<IActionResult> AddAsync(TWriteDto dto)
        {
            var result = await BaseService.AddAsync(dto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [NonAction]
        public async Task<IActionResult> UpdateAsync(int id, TWriteDto dto)
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
