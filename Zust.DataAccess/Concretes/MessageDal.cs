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
    public class MessageDal : EFEntityRepositoryBase<Message, SocialNetworkDbContext>, IMessageDal
    {
        public MessageDal(SocialNetworkDbContext context) : base(context)
        {
        }
    }
}
