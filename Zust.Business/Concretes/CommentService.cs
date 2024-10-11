using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstrats;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.Business.Concretes
{
   public class CommentService:ICommentService
    {
        private ICommentDal _commentDal;

        public CommentService(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task<List<CommentDTO>> GetCommentsForPostAsync(int postId, string currentUserId)
        {
        return await _commentDal.GetRecentCommentsForPostAsync(postId,currentUserId);
        }

        public async Task AddComment(Comment newComment)
        {
            _commentDal.AddCommentAsync(newComment);
        }


    }
}
