using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstrats;
using Zust.DataAccess.Abstracts;
using Zust.Entities.Models;

namespace Zust.WebUI.Hubs
{
    public class StatusHub:Hub
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private IUserService userService;

        public StatusHub(UserManager<CustomIdentityUser> userManager, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            this.userService = userService;
        }

        //public async Task NotifyUserStatusChange(string userId, bool isOnline)
        //{
        //    await Clients.User(userId).SendAsync("ReceiveUserStatus", userId, isOnline);
        //}

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var userItem = await userService.GetAsync(u => u.Id == user.Id);
            if (userItem != null)
            {
                userItem.isOnline = true;
                await userService.UpdateAsync(userItem);
            }

            await Clients.Others.SendAsync("ReceiveUserStatus", new { userId = user.Id, isOnline = true });
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
