﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.Abstracts;
using Zust.Entities.Models;

namespace Zust.DataAccess.Abstracts
{
    public interface IMessageDal:IEntityRepository<Message>
    {
    }
}
