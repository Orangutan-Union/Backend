namespace TECHUB.Repository.Models
{
    public class FriendFollower
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }

        public virtual User? User { get; set; }
        public virtual User? OtherUser { get; set; }
    }
}
