using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.Business.Abstrats
{
    public interface ICommentService
    {
        public Task<List<CommentDTO>> GetCommentsForPostAsync(int postId, string currentUserId);
        public Task AddComment(Comment newComment);
    }
}
