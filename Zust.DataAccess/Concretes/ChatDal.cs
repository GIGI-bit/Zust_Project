﻿using Microsoft.EntityFrameworkCore;
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
    public class ChatDal : EFEntityRepositoryBase<Chat, SocialNetworkDbContext>, IChatDal
    {
        private readonly SocialNetworkDbContext _context;
        public ChatDal(SocialNetworkDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Chat> GetChat(string senderId,string recieverId)
        {
          return await _context.Chats.Include(nameof(Chat.Messages)).FirstOrDefaultAsync(c => c.SenderId == senderId && c.ReceiverId == recieverId||
                       c.SenderId == recieverId && c.ReceiverId == senderId);
        }

        public  List<Chat> GetChatsWithReciever(string id)
        {
           return  _context.Chats.Include(nameof(Chat.Messages)).Include(nameof(Chat.Receiver)).Where(c => c.SenderId == id || c.ReceiverId == id).ToList();
        }
    }
}
