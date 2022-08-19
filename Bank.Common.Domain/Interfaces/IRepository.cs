using System.Linq.Expressions;

namespace Bank.Common.Domain.Interfaces
{
    internal interface IRepository<T> : IDisposable where T : class
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true);
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);

    }
}
