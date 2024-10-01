using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zust.Entities.Models;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<CustomIdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<CustomIdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult Notification()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Messages() { return View(); }
        public IActionResult Friends() { return View(); }
        public IActionResult Events() { return View(); }
        public IActionResult Login() { return View(); }
        public IActionResult Register() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
