using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Zust.Business.Abstrats;
using Zust.Entities.Models;

namespace Zust.WebUI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private IUserService _userService;

        public ChatHub(UserManager<CustomIdentityUser> userManager, IHttpContextAccessor contextAccessor, IUserService userService)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var usersList = await _userService.GetListAsync();

            var userItem= usersList.SingleOrDefault(u => u.Id == user.Id);

            userItem.isOnline= true;
            userItem.ConnectTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
           

            string info = user.UserName + " connected successfully";
            await Clients.Others.SendAsync("Connect", info);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            var usersList = await _userService.GetListAsync();

            var userItem = usersList.SingleOrDefault(u => u.Id == user.Id);
            userItem.isOnline = false;
            userItem.DisconnectTime = DateTime.Now;
       
            string info = user.UserName + " disconnected successfully";
            await Clients.Others.SendAsync("Disconnect", info);
        }

        public async Task SendFollow(string id)
        {
            await Clients.User(id).SendAsync("ReceiveNotification");
        }

        public async Task GetMessages(string receiverId, string senderId)
        {
            await Clients.Users(new String[] { receiverId, senderId }).SendAsync("ReceiveMessages", receiverId, senderId);
        }
    }
}
