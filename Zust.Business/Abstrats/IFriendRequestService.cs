using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface IFriendRequestService
    {
        Task<FriendRequest> GetAsync(Expression<Func<FriendRequest, bool>> filter);
        Task<List<FriendRequest>> GetListAsync();
        Task DeleteListAsync(List<FriendRequest> entities);

        Task DeleteAsync(FriendRequest entity);
        Task AddAsync(FriendRequest entity);
        Task UpdateAsync(FriendRequest entity);
    }
}
