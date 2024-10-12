using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface IMessageService
    {
        Task<Message> GetAsync(Expression<Func<Message, bool>> filter);
        Task<List<Message>> GetListAsync();
        Task DeleteAsync(Message entity);
        Task AddAsync(Message entity);
        Task UpdateAsync(Message entity);
    }
}
