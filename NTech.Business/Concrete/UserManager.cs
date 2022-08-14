using Core.Entity.Concrete;
using Core.Utilities.Result;
using Core.Utilities.ResultMessage;
using Microsoft.EntityFrameworkCore;
using NTech.Business.Abstract;
using NTech.DataAccess.Abstract;
using NTech.DataAccess.UnitOfWork.Abstract;

namespace NTech.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILanguageMessage _languageMessage;

        public UserManager(IUnitOfWork unitOfWork, IUserDal userDal, ILanguageMessage languageMessage)
        {
            _unitOfWork = unitOfWork;
            _userDal = userDal;
            _languageMessage = languageMessage;
        }

        public async Task<IResult> AddAsync(User user)
        {
            await _userDal.AddAsync(user);
            int row = await _unitOfWork.CompleteAsync();
            await AddToRoleAsync(user.Id, "User");
            return row > 0 ?
                new SuccessResult(_languageMessage.SuccessfullyAdded) :
                new ErrorResult(_languageMessage.FailedToAdd);
        }

        public async Task<IResult> AddToRoleAsync(int userId, int roleId)
        {
            await _userDal.AddToRoleAsync(userId, roleId);
            int row = await _unitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(_languageMessage.SuccessfullyAdded) :
                new ErrorResult(_languageMessage.FailedToAdd);
        }

        public async Task<IResult> AddToRoleAsync(int userId, string roleName)
        {
            await _userDal.AddToRoleAsync(userId, roleName);
            int row = await _unitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(_languageMessage.SuccessfullyAdded) :
                new ErrorResult(_languageMessage.FailedToAdd);
        }

        public async Task<IDataResult<User>> GetByEmailAsync(string email)
        {
            var result = await _userDal.GetAsync(x => x.Email.ToLower() == email.ToLower());
            if (result == null)
                return new ErrorDataResult<User>(_languageMessage.UserNotFound);
            return new SuccessDataResult<User>(result);
        }

        public async Task<IDataResult<User>> GetByIdAsync(int id)
        {
            var result = await _userDal.GetAsync(x => x.Id == id);
            if (result == null)
                return new ErrorDataResult<User>(_languageMessage.UserNotFound);
            return new SuccessDataResult<User>(result);
        }

        public async Task<IDataResult<List<User>>> GetListAsync()
        {
            var result = await _userDal.GetAll().ToListAsync();
            return new SuccessDataResult<List<User>>(result, _languageMessage.SuccessfullyListed);
        }

        public async Task<List<Role>> GetRolesAsync(int userId)
        {
            var result = await _userDal.GetRolesAsync(userId);
            return result;
        }

        public async Task<IResult> UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
            int row = await _unitOfWork.CompleteAsync();
            return row > 0 ?
                new SuccessResult(_languageMessage.SuccessfullyUpdated) :
                new ErrorResult(_languageMessage.FailedToUpdate);
        }
    }
}
