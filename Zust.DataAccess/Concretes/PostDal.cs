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
    public class PostDal : EFEntityRepositoryBase<Post, SocialNetworkDbContext>, IPostDal
    {
        private SocialNetworkDbContext _context;
        public PostDal(SocialNetworkDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PostDTO>> GetFullPostsList()
        {
            var posts = await _context.Posts
            .Where(p => p.Status == "Public")
            .Select(p => new PostDTO
            {
                PostId = p.Id,
                Message = p.Text,
                PostImage = p.ImageLink,
                CreatedAt = p.CreateDate,
                User = new UserDTO
                {
                    UserName = p.Publisher.UserName,
                    UserProfileImage = p.Publisher.ProfileImageUrl,
                    UserId = p.PublisherId
                },
                LikedUsers = p.Likes.Select(l => new UserDTO
                {
                    UserId = l.Id,
                    UserName = l.UserName,
                    UserProfileImage = l.ProfileImageUrl
                }).ToList(),
                LikeCount = p.Likes.Count(),
                Comments = p.Comments.Select(c => new CommentDTO
                {
                    CommentText = c.Content,
                    User = new UserDTO
                    {
                        UserName = c.User.UserName,
                        UserProfileImage = c.User.ProfileImageUrl,
                        UserId = c.UserId
                    },
                    CommentedAt = c.CreatedAt,
                }).ToList(),
                //TimeAgo = GetTimeAgo(p.CreateDate),
            })
            .ToListAsync();
            return posts;
        }

        public async Task<PostDTO> RemoveLike(CustomIdentityUser user, int postId)
        {
            var post = await _context.Posts
                             .Include(p => p.Likes)
                             .FirstOrDefaultAsync(p => p.Id == postId);
            if (post.Likes.Contains(user))
            {
                post.Likes.Remove(user);
            }
            else
            {
                post.Likes.Add(user);
            }

            await _context.SaveChangesAsync();

            var postDto = new PostDTO
            {
                PostId = post.Id,
                LikeCount = post.Likes.Count,
                IsLiked = post.Likes.Any(u => u.Id == user.Id),
                LikedUsers = post.Likes.Select(u => new UserDTO
                {
                    UserId = u.Id,
                    UserName = u.UserName,

                }).ToList()
            };
            return postDto;
        }

        //public string GetTimeAgo(DateTime createdAt)
        //{
        //    TimeSpan timeSpan = DateTime.UtcNow - createdAt;

        //    if (timeSpan.TotalMinutes < 60)
        //    {
        //        return $"{(int)timeSpan.TotalMinutes} minutes ago";
        //    }
        //    else if (timeSpan.TotalHours < 24)
        //    {
        //        return $"{(int)timeSpan.TotalHours} hours ago";
        //    }
        //    else
        //    {
        //        return $"{(int)timeSpan.TotalDays} days ago";
        //    }
        //}
    }
}
