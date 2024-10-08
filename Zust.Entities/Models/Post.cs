using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entities.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Text {  get; set; }
        public string? ImageLink { get; set; }
        public string? VideoLink { get; set; }
        public string PublisherId { get; set; }
        public virtual CustomIdentityUser Publisher { get; set; } 
        public virtual List<CustomIdentityUser> Likes { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
