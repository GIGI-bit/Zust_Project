using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Concretes;
using Zust.DataAccess.Abstracts;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concretes
{
    public class CommentDal : EFEntityRepositoryBase<Comment, SocialNetworkDbContext>, ICommentDal
    {
        private SocialNetworkDbContext _context;

        public CommentDal(SocialNetworkDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<CommentDTO>> GetRecentCommentsForPostAsync(int postId,string currentUserId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt) 
                .Take(3) 
                .Select(c => new CommentDTO
                {
                    CommentText = c.Content,
                    User = new UserDTO { UserName= c.User.UserName }, 
                    IsCurrentUser = c.UserId == currentUserId, 
                    CommentedAt = c.CreatedAt
                })
                .ToListAsync();
        }


        public async Task AddCommentAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

    }
}
