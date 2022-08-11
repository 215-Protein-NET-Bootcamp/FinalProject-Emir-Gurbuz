using AutoMapper;
using Core.DataAccess;
using Core.Dto;
using Core.Entity;
using Core.Utilities.Result;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;

namespace NTech.Business.Concrete
{
    public class AsyncBaseService<TEntity, TDto> : IAsyncBaseService<TEntity, TDto>
        where TEntity : class, IEntity, new()
        where TDto : class, IDto, new()
    {

        protected readonly IAsyncRepository<TEntity> Repository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;
        public AsyncBaseService(IAsyncRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            Repository = repository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public async Task<IResult> AddAsync(TDto dto)
        {
            TEntity addedEntity = Mapper.Map<TEntity>(dto);
            await Repository.AddAsync(addedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult() :
                new ErrorResult();
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            TEntity deletedEntity = await Repository.GetAsync(x => x.Id == id);
            if (deletedEntity == null)
                return new ErrorDataResult<TDto>();

            await Repository.DeleteAsync(deletedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult() :
                new ErrorResult();
        }

        public async Task<IDataResult<TDto>> GetByIdAsync(int id)
        {
            TEntity entity = await Repository.GetAsync(x => x.Id == id);
            if (entity == null)
                return new ErrorDataResult<TDto>();

            TDto returnEntity = Mapper.Map<TDto>(entity);
            return new SuccessDataResult<TDto>(returnEntity);
        }

        public async Task<IDataResult<List<TDto>>> GetListAsync()
        {
            List<TEntity> entities = await Repository.GetAll().ToListAsync();
            List<TDto> returnEntities = Mapper.Map<List<TDto>>(entities);

            return new SuccessDataResult<List<TDto>>(returnEntities);
        }

        public async Task<IResult> UpdateAsync(int id, TDto dto)
        {
            TEntity updatedEntity = await Repository.GetAsync(x => x.Id == id);
            if (updatedEntity == null)
                return new ErrorDataResult<TDto>();

            Mapper.Map(dto, updatedEntity);
            await Repository.UpdateAsync(updatedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult() :
                new ErrorResult();
        }
    }
}
