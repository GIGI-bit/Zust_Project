using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstrats;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.Concretes;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.Business.Concretes
{
    public class FriendService:IFriendService
    {
        private readonly IFriendDal _friendDal;

        public FriendService(IFriendDal friendDal)
        {
            _friendDal = friendDal;
        }

        public async Task AddAsync(Friend entity)
        {
            await _friendDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Friend entity)
        {
            await _friendDal.DeleteAsync(entity);
        }

        public async Task<Friend> GetAsync(Expression<Func<Friend, bool>> filter)
        {
            return await _friendDal.GetAsync(filter);
        }

        public async Task<List<Friend>> GetListAsync()
        {
            return await _friendDal.GetListAsync();
        }

        public async Task UpdateAsync(Friend entity)
        {
await _friendDal.UpdateAsync(entity);
        }
    }
}
