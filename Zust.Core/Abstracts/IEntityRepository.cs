using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Zust.Core.Abstracts
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetListAsync();
        Task DeleteListAsync(List<T> entities);
  
        Task DeleteAsync(T entity);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
