using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstracts;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstracts
{
    public interface ICommentDal :IEntityRepository<Comment>
    {
        public Task AddCommentAsync(Comment comment);
        public Task<List<CommentDTO>> GetRecentCommentsForPostAsync(int postId, string currentUserId);


    }
}
