using Bank.Common.Domain.Interfaces;
using Bank.Transaction.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bank.Transaction.Persistence
{
    internal class Repository<T> : IRepository<T> where T : class
    {
        private TransactionDbContext _context;
        private DbSet<T> _dbSet;

        public Repository(TransactionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity) => await Task.FromResult(_dbSet.Remove(entity));
        public void Dispose() => _context?.Dispose();

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task UpdateAsync(T entity)
        => await Task.FromResult(_context.Entry(entity).State = EntityState.Modified);
    }
}
