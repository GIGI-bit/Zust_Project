using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Zust.Business.Abstrats;
using Zust.Business.Concretes;
using Zust.DataAccess.DTOs;
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
        private readonly IImageService _imageService;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public HomeController(ILogger<HomeController> logger, UserManager<CustomIdentityUser> userManager, IUserService userService, IImageService imageService,IPostService postService,ICommentService commentService)
        {
            _logger = logger;
            _userManager = userManager;
            this.userService = userService;
            _imageService = imageService;
            _postService = postService;
            _commentService = commentService;
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

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                Post post = new Post
                {
                    PublisherId = user.Id,
                    Text = model.Message
                };
              
                if (model.ImgFile != null)
                {
                    post.ImageLink= await _imageService.SaveFile(model.ImgFile, "posts");

                }
                if (model.VideoFile!= null)
                {
                //post.VideoLink=await 
                
                }
                await _postService.AddAsync(post);
                

             }
            return Json(new { success = true, message = "Post created successfully!" });
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

        public async Task<IActionResult> GetPosts()
        {
            var sortedPosts = await _postService.GetFullPostsList();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            foreach (var post in sortedPosts)
            {
                post.IsCurrentUser = user.Id == post.User.UserId ? true : false;
                post.IsLiked=post.LikedUsers.Contains(new DataAccess.DTOs.UserDTO { UserId=user.Id,UserName=user.UserName,UserProfileImage=user.ProfileImageUrl});
                foreach (var comment in post.Comments)
                {
                    comment.IsCurrentUser=user.Id == comment.User.UserId?true:false;
                    
                }

            }




            return Json(sortedPosts);
        }
        [HttpPost]
        public async Task<IActionResult> ToggleLike(int postId)
        {
            var user = await _userManager.GetUserAsync(User); 

            if (user == null)
            {
                return Unauthorized();
            }

            var updatedPost = await _postService.ToggleLikeAsync(user, postId);

            if (updatedPost == null)
            {
                return NotFound();
            }

            return Json(new { success = true, likeCount = updatedPost.LikeCount, isLiked = updatedPost.IsLiked });
        }



        [HttpGet]
        public async Task<IActionResult> GetRecentComments(int postId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var recentComments =await _commentService.GetCommentsForPostAsync(postId,user.Id);
          
            return Json(recentComments);  
        }

        [HttpPost]
        public async Task<IActionResult>AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var currentUser =await _userManager.GetUserAsync(User);

               
                var newComment = new Comment
                {
                    PostId = model.PostId,
                    Content= model.Message,
                    CreatedAt = DateTime.UtcNow,
                    UserId = currentUser.Id  
                };

               await _commentService.AddComment(newComment);

                
                var recentComments = await _commentService.GetCommentsForPostAsync(model.PostId, currentUser.Id);


                return Json(recentComments);
            }
            return BadRequest("Invalid comment data.");
        }


        public IActionResult GetCreatePostFormViewComponent()
        {
            return ViewComponent("PostForm");
        }

        public IActionResult Events() { return View(); }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
