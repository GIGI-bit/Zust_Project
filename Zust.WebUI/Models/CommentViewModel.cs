namespace Zust.WebUI.Models
{
    public class CommentViewModel
    {
        public int PostId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserName { get; set; }
        public string UserImage { get; set; }
    }

}
