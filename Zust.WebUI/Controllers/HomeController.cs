using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zust.Business.Abstrats;
using Zust.Entities.Models;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly IUserService userService;

        public HomeController(ILogger<HomeController> logger, UserManager<CustomIdentityUser> userManager, IUserService userService)
        {
            _logger = logger;
            _userManager = userManager;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        public async Task<IActionResult> GetAllUsers()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var users = await userService.GetListAsync();
            var userList=users
               .Where(u => u.Id != user.Id)
               .OrderByDescending(u => u.isOnline)
               .Select(u => new CustomIdentityUser
               {
                   Id = u.Id,
                   UserName = u.UserName,
                   isOnline = u.isOnline,
                   Email = u.Email,
               }).ToList();

           

            return Ok(userList);

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
        public async Task<IActionResult> Friends()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
           
            return View(); 
        }
        public IActionResult Events() { return View(); }
   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
