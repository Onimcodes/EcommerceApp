using IdentityService.domain.Common.Models;
using System.Linq.Expressions;

namespace IdentityService.application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : IDbEntity
    {
        Task<T> GetByIdAsync(string id);
        Task<List<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}
