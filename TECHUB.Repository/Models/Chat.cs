namespace TECHUB.Repository.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public string ChatName { get; set; } = string.Empty;
        public DateTime LastMessageSent { get; set; }
        public bool IsPrivate { get; set; }

        public virtual List<User> Users { get; set; } = new List<User>();
        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
