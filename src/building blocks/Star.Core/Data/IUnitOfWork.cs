using System.Threading.Tasks;

namespace Star.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}