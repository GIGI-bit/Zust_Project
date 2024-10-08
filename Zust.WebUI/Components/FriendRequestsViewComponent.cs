using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Components
{
    public class FriendRequestsViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
           

            return View();
        }
    }
}
