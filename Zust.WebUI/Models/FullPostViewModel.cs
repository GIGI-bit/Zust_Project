namespace Zust.WebUI.Models
{
    public class FullPostViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string PostImage { get; set; }
        public DateTime CreatedAt { get; set; }

        public string UserName { get; set; }
        public string UserImage { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
