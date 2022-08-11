namespace NTech.DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CompleteAsync();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
