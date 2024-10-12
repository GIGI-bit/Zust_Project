using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstracts;

namespace Zust.Entities.Models
{
    public class Friend:IEntity
    {
        public int Id { get; set; }
        public string? OwnId { get; set; }
        public string? YourFriendId { get; set; }
        public virtual CustomIdentityUser? YourFriend { get; set; }
    }
}
