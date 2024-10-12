using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstracts;

namespace Zust.Entities.Models
{
    public class FriendRequest:IEntity
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public string? Status { get; set; }
        public string? SenderId { get; set; }
        public virtual CustomIdentityUser? Sender { get; set; }
        public string? ReceiverId { get; set; }
    }
}
