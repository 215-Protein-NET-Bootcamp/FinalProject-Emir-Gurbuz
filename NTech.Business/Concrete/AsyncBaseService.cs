using AutoMapper;
using Core.Aspect.Autofac.Caching;
using Core.DataAccess;
using Core.Dto;
using Core.Entity;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NTech.Business.Abstract;
using NTech.DataAccess.Contexts;
using NTech.DataAccess.UnitOfWork.Abstract;

namespace NTech.Business.Concrete
{
    public class AsyncBaseService<TEntity, TWriteDto, TReadDto> : IAsyncBaseService<TEntity, TWriteDto, TReadDto>
        where TEntity : class, IEntity, new()
        where TWriteDto : class, IWriteDto, new()
        where TReadDto : class, IReadDto, new()
    {

        protected readonly IAsyncRepository<TEntity> Repository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly ILanguageMessage LanguageMessage;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public AsyncBaseService(IAsyncRepository<TEntity> repository, IMapper mapper, IUnitOfWork unitOfWork, ILanguageMessage languageMessage)
        {
            Repository = repository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            LanguageMessage = languageMessage;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }
        public virtual async Task<IResult> AddAsync(TWriteDto dto)
        {
            TEntity addedEntity = Mapper.Map<TEntity>(dto);

            int userId = _httpContextAccessor.HttpContext.User.ClaimNameIdentifier();
            addedEntity.SetUserId(userId);

            await Repository.AddAsync(addedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(LanguageMessage.SuccessfullyAdded) :
                new ErrorResult(LanguageMessage.FailedToAdd);
        }

        public virtual async Task<DataResult<TReadDto>> GetByIdAsync(int id)
        {
            TEntity entity = await Repository.GetAsync(x => x.Id == id);
            if (entity == null)
                return new ErrorDataResult<TReadDto>();

            TReadDto returnEntity = Mapper.Map<TReadDto>(entity);
            return new SuccessDataResult<TReadDto>(returnEntity, LanguageMessage.SuccessfullyGet);
        }
        public virtual async Task<DataResult<List<TReadDto>>> GetListAsync()
        {
            List<TEntity> entities = await Repository.GetAll().ToListAsync();
            List<TReadDto> returnEntities = Mapper.Map<List<TReadDto>>(entities);

            return new SuccessDataResult<List<TReadDto>>(returnEntities, LanguageMessage.SuccessfullyListed);
        }

        public virtual async Task<IResult> HardDeleteAsync(int id)
        {
            TEntity deletedEntity = await Repository.GetAsync(x => x.Id == id);
            if (deletedEntity == null)
                return new ErrorResult();

            await Repository.DeleteAsync(deletedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(LanguageMessage.SuccessfullyDeleted) :
                new ErrorResult(LanguageMessage.FailedToDelete);
        }

        public virtual async Task<IResult> SoftDeleteAsync(int id)
        {
            TEntity deletedEntity = await Repository.GetAsync(x => x.Id == id);
            if (deletedEntity == null)
                return new ErrorResult();

            deletedEntity.DeletedDate = DateTime.Now;
            await Repository.UpdateAsync(deletedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(LanguageMessage.SuccessfullyDeleted) :
                new ErrorResult(LanguageMessage.FailedToDelete);
        }

        public virtual async Task<IResult> UpdateAsync(int id, TWriteDto dto)
        {
            TEntity updatedEntity = await Repository.GetAsync(x => x.Id == id);
            if (updatedEntity == null)
                return new ErrorDataResult<TWriteDto>();

            Mapper.Map(dto, updatedEntity);
            await Repository.UpdateAsync(updatedEntity);

            int row = await UnitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(LanguageMessage.SuccessfullyUpdated) :
                new ErrorResult(LanguageMessage.FailedToUpdate);
        }
    }
}
