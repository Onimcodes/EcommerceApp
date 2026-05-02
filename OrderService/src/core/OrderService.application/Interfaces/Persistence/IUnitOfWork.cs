namespace OrderService.application.Interfaces.Persistence
{
    public interface IUnitOfWork : IDisposable
    {

        IOrderRepository Orders { get; }
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        int Complete();
        Task ReloadEntityAsync<TEntity>(TEntity entity) where TEntity : class;
    }
}
