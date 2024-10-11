using Zust.DataAccess.DTOs;

namespace Zust.WebUI.Models
{
    public class PostViewModel
    {
        public string Message { get; set; }
        public IFormFile? ImgFile { get; set; }
        public IFormFile? VideoFile { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
