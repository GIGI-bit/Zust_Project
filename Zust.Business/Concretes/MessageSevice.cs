using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstrats;
using Zust.DataAccess.Abstracts;
using Zust.Entities.Models;

namespace Zust.Business.Concretes
{
    public class MessageSevice:IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageSevice(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public async Task AddAsync(Message entity)
        {
            await _messageDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Message entity)
        {
            await _messageDal.DeleteAsync(entity);
        }

        public async Task<Message> GetAsync(Expression<Func<Message, bool>> filter)
        {
            return await _messageDal.GetAsync(filter);
        }

        public async Task<List<Message>> GetListAsync()
        {
            return await _messageDal.GetListAsync();
        }

        public async Task UpdateAsync(Message entity)
        {
            await _messageDal.UpdateAsync(entity);
        }
    }
}
