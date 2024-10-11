using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstracts;

namespace Zust.Entities.Models
{
    public class Comment:IEntity
    {
        public int Id { get; set; }
        public string? Content { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
        public string UserId { get; set; } 
        public int PostId { get; set; } 
        public virtual CustomIdentityUser User { get; set; } 
        public virtual Post Post { get; set; }

    }
}
