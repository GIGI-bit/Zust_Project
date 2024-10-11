using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface IPostService
    {
        Task<Post> GetAsync(Expression<Func<Post, bool>> filter);
        Task<List<Post>> GetListAsync();
        Task<List<PostDTO>> GetFullPostsList();

        public Task<PostDTO> ToggleLikeAsync(CustomIdentityUser user, int postId);
        Task DeleteListAsync(List<Post> entities);
        Task DeleteAsync(Post entity);
        Task AddAsync(Post entity);
        Task UpdateAsync(Post entity);
    }
}
