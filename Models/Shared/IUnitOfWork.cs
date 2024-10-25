namespace TodoApi.Models.Shared
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}