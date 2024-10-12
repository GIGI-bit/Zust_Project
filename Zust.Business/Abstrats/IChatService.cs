using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Zust.DataAccess.Abstracts;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface IChatService
    {
          Task<Chat> GetAsync(Expression<Func<Chat, bool>> filter);
        Task<List<Chat>> GetListAsync();
        Task DeleteAsync(Chat entity);
        Task AddAsync(Chat entity);
        Task UpdateAsync(Chat entity);
        public Task<Chat> GetChat(string id);
        public List<Chat> GetChatsWithReciever(string id);


    }
}
