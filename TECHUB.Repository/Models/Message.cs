namespace TECHUB.Repository.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; } = string.Empty;
        public virtual User? User { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
