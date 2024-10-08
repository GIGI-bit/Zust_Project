using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface IUserService
    {
        Task<CustomIdentityUser> GetAsync(Expression<Func<CustomIdentityUser, bool>> filter);
        Task<List<CustomIdentityUser>> GetListAsync();
        Task DeleteListAsync(List<CustomIdentityUser> entities);
        Task DeleteAsync(CustomIdentityUser entity);
        Task AddAsync(CustomIdentityUser entity);
        Task UpdateAsync(CustomIdentityUser entity);
    }
}
