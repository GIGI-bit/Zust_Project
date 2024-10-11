using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.DataAccess.DTOs
{
    public class CommentDTO
    {
        public string CommentText { get; set; }
public UserDTO User { get; set; }
        public bool IsCurrentUser {  get; set; }
        public DateTime CommentedAt { get; set; }

    }
}
