namespace IdentityService.application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IUserRepository Users { get;  }
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        int Complete();
        Task ReloadEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
