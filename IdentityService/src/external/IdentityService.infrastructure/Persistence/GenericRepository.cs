using IdentityService.application.Interfaces.Persistence;
using IdentityService.domain.Common.Models;
using IdentityService.infrastructure.Common.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.infrastructure.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IDbEntity
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public void Add(T entity)
        {
            _appDbContext.Add(entity);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Where(expression).AsEnumerable();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Remove(T entity)
        {
            _appDbContext.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _appDbContext.RemoveRange(entities);
            _appDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _appDbContext.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}
