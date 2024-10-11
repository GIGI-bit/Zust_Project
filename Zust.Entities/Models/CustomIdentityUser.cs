
using Microsoft.AspNetCore.Identity;
using System;
using Zust.Core.Abstracts;

namespace Zust.Entities.Models
{
    public class CustomIdentityUser:IdentityUser, IEntity
    {
        public string? PublicImgURL { get; set; } = "~/assets/images/my-profile-bg.jpg";
        public string? ProfileImageUrl { get; set; }
        public bool IsFriend { get; set; }
        public bool HasRequestPending { get; set; }
        public string? City { get; set; }
        public bool isOnline { get; set; }
        public DateTime DisconnectTime { get; set; }= DateTime.Now;
        public string ConnectTime { get; set; } = "";
        public virtual ICollection<Friend>? Friends { get; set; }
        public virtual ICollection<FriendRequest>? FriendRequests { get; set; }
        //public virtual ICollection<Chat>? Chats { get; set; }
        public CustomIdentityUser()
        {
            Friends = new List<Friend>();
            FriendRequests = new List<FriendRequest>();
            //Chats = new List<Chat>();
        }

    }
}
