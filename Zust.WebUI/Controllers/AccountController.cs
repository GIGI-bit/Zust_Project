using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstrats;
using Zust.Entities.Models;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IUserService userService;
        private readonly IImageService _imageService;


        public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager, IUserService userService,IImageService imageService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            this.userService = userService;
            _imageService = imageService;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user=await userService.GetAsync(u=>u.UserName==model.Username);
                    user.isOnline = true;
                    await userService.UpdateAsync(user);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Login");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    model.ImageUrl = await _imageService.SaveFile(model.File,"user");
                }

                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    City = model.City,
                    isOnline=true,
                    ConnectTime=DateTime.Now.ToString(),
                    ProfileImageUrl=model.ImageUrl,
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    CustomIdentityRole role = new CustomIdentityRole
                    {
                        Name = "User"
                    };
                    IdentityResult roleResult = await _roleManager.CreateAsync(role);
                    if (!roleResult.Succeeded) { ModelState.AddModelError("", "We couldn't assign you a role!"); }
                    ///
                }
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
    }
}
