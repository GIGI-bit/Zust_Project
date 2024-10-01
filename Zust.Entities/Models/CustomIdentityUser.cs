
using Microsoft.AspNetCore.Identity;
using Zust.Core.Abstracts;

namespace Zust.Entities.Models
{
    public class CustomIdentityUser:IdentityUser, IEntity
    {
        public string? PublicImgURL { get; set; }
        public string? City { get; set; }
        public bool isOnline { get; set; }
        public DateTime DisconnectTime { get; set; }= DateTime.Now;
        public string ConnectTime { get; set; } = "";

    }
}
