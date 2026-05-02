using OrderService.application.Interfaces.Persistence;
using OrderService.infrastructure.Common.DataContext;

namespace OrderService.infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Orders = new OrderRepository(_context);
        }

       public IOrderRepository Orders { get; set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ReloadEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            var entry = _context.Entry(entity);
            if (entry != null)
            {
                await entry.ReloadAsync();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
