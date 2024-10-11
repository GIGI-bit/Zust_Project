using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entities.Models;

namespace Zust.DataAccess.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public string Message { get; set; }
        public string PostImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO User {  get; set; }
      public int LikeCount { get; set; }
        public List<UserDTO> LikedUsers { get; set; }
        public bool IsLiked { get; set; }
        public string TimeAgo { get; set; } 
        public bool IsCurrentUser {  get; set; }
        public List<CommentDTO> Comments { get; set; }
        public PostDTO()
        {
            TimeAgo = CreatedAt.ToString();
        }
    }
}

