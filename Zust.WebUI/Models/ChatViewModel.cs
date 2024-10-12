namespace Zust.WebUI.Models
{
    public class ChatViewModel
    {
        public string? CurrentUserId { get; set; }
        public ChatBlockViewModel? CurrentChat { get; set; }
        public IEnumerable<ChatBlockViewModel>? Chats { get; set; }
        public string? CurrentReceiver { get; internal set; }
    }
}
