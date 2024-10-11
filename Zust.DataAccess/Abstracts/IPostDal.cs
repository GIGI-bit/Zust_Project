using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstracts;
using Zust.DataAccess.DTOs;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstracts
{
  public interface IPostDal: IEntityRepository<Post>
    {
        Task<List<PostDTO>> GetFullPostsList();
        Task<PostDTO> RemoveLike(CustomIdentityUser user,int postId);


    }
}
