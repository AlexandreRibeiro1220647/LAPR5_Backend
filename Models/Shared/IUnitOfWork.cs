using System.Threading.Tasks;

namespace Models.Shared
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}