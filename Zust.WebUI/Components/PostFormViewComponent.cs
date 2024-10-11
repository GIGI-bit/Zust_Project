using Microsoft.AspNetCore.Mvc;

namespace Zust.WebUI.Components
{
    public class PostFormViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
