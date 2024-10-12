using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Concretes;
using Zust.DataAccess.Abstracts;
using Zust.Entities.Models;

namespace Zust.DataAccess.Concretes
{
    public class FriendDal : EFEntityRepositoryBase<Friend, SocialNetworkDbContext>, IFriendDal
    {
        public FriendDal(SocialNetworkDbContext context) : base(context)
        {
        }
    }
}
