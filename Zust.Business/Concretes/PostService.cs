using Microsoft.EntityFrameworkCore;
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
    public class PostService:IPostService
    {
        private readonly IPostDal _postDal;

        public PostService(IPostDal postDal) { _postDal = postDal; }


        public async Task UpdateAsync(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post), "post cannot be null");
            }

            await _postDal.UpdateAsync(post);
        }

        public async Task DeleteAsync(Post post)
        {
            if (post == null)
            {
                throw new ArgumentException("Invalid post ID");
            }
            await _postDal.DeleteAsync(post);
        }

        public async Task<Post> GetAsync(Expression<Func<Post, bool>> filter)
        {
            return await _postDal.GetAsync(filter);
        }

        public async Task<List<Post>> GetListAsync()
        {
            return await _postDal.GetListAsync();
        }

        public Task DeleteListAsync(List<Post> entities)
        {
            throw new NotImplementedException();
        }



        public async Task AddAsync(Post entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "post cannot be null");
            }

            await _postDal.AddAsync(entity);
        }

        public async Task<List<PostDTO>> GetFullPostsList()
        {
           return await _postDal.GetFullPostsList();
        }

        public async Task<PostDTO> ToggleLikeAsync(CustomIdentityUser user, int postId)
        {
            return await _postDal.RemoveLike(user, postId);
        }
    }
}
