using System.ComponentModel.DataAnnotations;

namespace Zust.WebUI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        
        public string? City{ get; set; }
        //public IFormFile? File {  get; set; }
        //public string? ImgUrl { get; set; } = "user.png";
    }
}
