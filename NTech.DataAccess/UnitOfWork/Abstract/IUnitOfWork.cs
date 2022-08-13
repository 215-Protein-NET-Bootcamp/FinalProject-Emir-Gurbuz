namespace NTech.DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
    }
}
