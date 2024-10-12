using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Components
{
    public class NotificationComponentViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
