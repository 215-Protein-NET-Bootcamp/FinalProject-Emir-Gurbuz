using Core.DataAccess;
using Core.Entity.Concrete;

namespace NTech.DataAccess.Abstract
{
    public interface IEmailQueueDal : IAsyncRepository<EmailQueue>
    {
    }
}
