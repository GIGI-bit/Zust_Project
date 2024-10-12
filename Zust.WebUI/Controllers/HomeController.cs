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
        private readonly IFriendRequestService _friendReqSer;
        private readonly IFriendService _friendService;
        private readonly IChatService _chatService;
        private readonly IMessageService _messageService;

        public HomeController(ILogger<HomeController> logger, UserManager<CustomIdentityUser> userManager, IUserService userService, IImageService imageService, IPostService postService, ICommentService commentService, IFriendRequestService friendRequestService, IFriendService friendService,IChatService chatService,IMessageService messageService)
        {
            _logger = logger;
            _userManager = userManager;
            this.userService = userService;
            _imageService = imageService;
            _postService = postService;
            _commentService = commentService;
            _friendReqSer = friendRequestService;
            _friendService = friendService;
            _chatService = chatService;
            _messageService = messageService;
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
            var allrequests = await _friendReqSer.GetListAsync();
            var myrequests = allrequests.Where(r => r.SenderId == user.Id);


            var allmyfriends = await _friendService.GetListAsync();
            var myfriends = allmyfriends.Where(f => f.OwnId == user.Id || f.YourFriendId == user.Id);

            var allusers = await userService.GetListAsync();
            var users = allusers
                .Where(u => u.Id != user.Id)
                .OrderByDescending(u => u.isOnline)
                .Select(u => new CustomIdentityUser
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    isOnline = u.isOnline,
                    ProfileImageUrl = u.ProfileImageUrl,
                    Email = u.Email,
                    HasRequestPending = (myrequests.FirstOrDefault(r => r.ReceiverId == u.Id && r.Status == "Request") != null),
                    IsFriend = myfriends.FirstOrDefault(f => f.OwnId == u.Id || f.YourFriendId == u.Id) != null
                })
                .ToList();
            return Ok(users);
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
                    post.ImageLink = await _imageService.SaveFile(model.ImgFile, "posts");

                }
                if (model.VideoFile != null)
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
        public async Task<IActionResult> GetAllRequests()
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var allRequests = await _friendReqSer.GetListAsync();
            var requests = allRequests.Where(r => r.ReceiverId == current.Id);

            return Ok(requests);
        }


        public async Task<IActionResult> Notification()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public async Task<IActionResult> Messages()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View(); }
        public async Task<IActionResult> Friends()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;

            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> TakeRequest(string id)
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var allrequest = await _friendReqSer.GetListAsync();
            var request = allrequest.FirstOrDefault(r => r.SenderId == current.Id && r.ReceiverId == id);
            if (request == null) return NotFound();
            await _friendReqSer.DeleteAsync(request);
            return Ok();
        }
        public async Task<IActionResult> GetPosts()
        {
            var sortedPosts = await _postService.GetFullPostsList();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            foreach (var post in sortedPosts)
            {
                post.IsCurrentUser = user.Id == post.User.UserId ? true : false;
                post.IsLiked = post.LikedUsers.Contains(new DataAccess.DTOs.UserDTO { UserId = user.Id, UserName = user.UserName, UserProfileImage = user.ProfileImageUrl });
                foreach (var comment in post.Comments)
                {
                    comment.IsCurrentUser = user.Id == comment.User.UserId ? true : false;

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
            var recentComments = await _commentService.GetCommentsForPostAsync(postId, user.Id);

            return Json(recentComments);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {

                var currentUser = await _userManager.GetUserAsync(User);


                var newComment = new Comment
                {
                    PostId = model.PostId,
                    Content = model.Message,
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

        public async Task<IActionResult> SendFollow(string id)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (receiverUser != null)
            {
                var request = new FriendRequest
                {
                    Content = $"{sender.UserName} sent friend request at {DateTime.Now.ToLongDateString()}",
                    SenderId = sender.Id,
                    Sender = sender,
                    ReceiverId = id,
                    Status = "Request"
                };
                await _friendReqSer.AddAsync(request);
                return Ok(request.Content.Replace(sender.UserName, "You"));
            }
            return BadRequest();
        }

        public async Task<IActionResult> GoChat(string id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var chat = await _chatService.GetChat(user.Id);

            if (chat == null)
            {
                chat = new Chat
                {
                    ReceiverId = id,
                    SenderId = user.Id,
                    Messages = new List<Message>()
                };

                await _chatService.AddAsync(chat);
            }

            var messages = chat.Messages;
            if (messages.Any())
            {
                foreach (var item in messages)
                {
                    item.HasSeen = true;
                    await _messageService.UpdateAsync(item);
                }
            }


            var chats = _chatService.GetChatsWithReciever(user.Id);
            var users = await userService.GetListAsync();
            var chatBlocks = from c in chats
                             let receiver = (user.Id != c.ReceiverId) ? c.Receiver :users.FirstOrDefault(u => u.Id == c.SenderId)
                             select new ChatBlockViewModel
                             {
                                 Messages = c.Messages,
                                 Id = c.Id,
                                 SenderId = c.SenderId,
                                 Receiver = receiver,
                                 ReceiverId = receiver.Id,
                                 UnReadMessageCount = c.Messages.Count(m => m.HasSeen == false)
                             };

            var result = chatBlocks.ToList().Where(c => c.ReceiverId != user.Id);

            var currentChatBlock = new ChatBlockViewModel
            {
                Id = chat.Id,
                Messages = messages,
                Receiver = chat.Receiver,
                ReceiverId = chat.ReceiverId,
                SenderId = chat.SenderId,
                UnReadMessageCount = chat.Messages.Count(m => m.HasSeen == false)
            };

            var model = new ChatViewModel
            {
                CurrentUserId = user.Id,
                CurrentReceiver = id,
                CurrentChat = currentChatBlock,
                Chats = result.Count() == 0 ? chatBlocks : result,
            };

            return View(model);
        }


        public async Task<IActionResult> AcceptRequest(string userId, string senderId, int requestId)
        {
            var receiverUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var sender = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == senderId);
            if (receiverUser != null)
            {
                var allrequest = await _friendReqSer.GetListAsync();
                var request = allrequest.FirstOrDefault(r => r.Id == requestId);

                await _friendReqSer.DeleteAsync(request);

                await _friendReqSer.AddAsync(new FriendRequest
                {
                    Content = $"{sender.UserName} accepted friend request at {DateTime.Now.ToLongDateString()} {DateTime.Now.ToShortTimeString()}",
                    SenderId = senderId,
                    ReceiverId = receiverUser.Id,
                    Sender = sender,
                    Status = "Notification"
                });

                await _friendService.AddAsync(new Friend
                {
                    OwnId = sender.Id,
                    YourFriendId = receiverUser.Id,
                });

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Unfollow(string id)
        {
            var current = await _userManager.GetUserAsync(HttpContext.User);
            var allfriend = await _friendService.GetListAsync();
            var friend = allfriend.FirstOrDefault(f => f.OwnId == current.Id && f.YourFriendId == id ||
            f.OwnId == id && f.YourFriendId == current.Id
            );

            if (friend == null) return NotFound();
            await _friendService.DeleteAsync(friend);
            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                var allrequest = await _friendReqSer.GetListAsync();
                var request = allrequest.FirstOrDefault(r => r.Id == id);
                await _friendReqSer.DeleteAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> DeclineRequest(int id, string senderId)
        {

            Console.WriteLine(id);
            try
            {
                var allrequests = await _friendReqSer.GetListAsync();
                var request = allrequests.FirstOrDefault(r => r.Id == id);
                await _friendReqSer.DeleteAsync(request);

                var current = await _userManager.GetUserAsync(HttpContext.User);
                var item = new FriendRequest
                {
                    SenderId = current.Id,
                    Sender = current,
                    ReceiverId = senderId,
                    Status = "Notification",
                    Content = $"{current.UserName} declined your friend request at {DateTime.Now.ToLongDateString()} {DateTime.Now.ToShortTimeString()}"
                };
                await _friendReqSer.AddAsync(item);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }

        public IActionResult Events() { return View(); }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
