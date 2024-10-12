using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstrats;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.Concretes;
using Zust.Entities.Models;

namespace Zust.Business.Concretes
{
    public class FriendRequestService : IFriendRequestService
    {
        private IFriendRequestDal _friendReqDal;

        public FriendRequestService(IFriendRequestDal friendReqDal)
        {
            _friendReqDal = friendReqDal;
        }

        public async Task AddAsync(FriendRequest entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Request cannot be null");
            }

            await _friendReqDal.AddAsync(entity);
        }

        public async Task DeleteAsync(FriendRequest entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Invalid ID");
            }
            await _friendReqDal.DeleteAsync(entity);
        }

        public Task DeleteListAsync(List<FriendRequest> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<FriendRequest> GetAsync(Expression<Func<FriendRequest, bool>> filter)
        {
            return await _friendReqDal.GetAsync(filter);
        }

        public async Task<List<FriendRequest>> GetListAsync()
        {
            return await _friendReqDal.GetListAsync();
        }

        public async Task UpdateAsync(FriendRequest entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Request cannot be null");
            }

            await _friendReqDal.UpdateAsync(entity);
        }
    }
}
