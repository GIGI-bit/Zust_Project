using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface IFriendService
    {

        Task<Friend> GetAsync(Expression<Func<Friend, bool>> filter);
        Task<List<Friend>> GetListAsync();
        Task DeleteAsync(Friend entity);
        Task AddAsync(Friend entity);
        Task UpdateAsync(Friend entity);
    }
}
