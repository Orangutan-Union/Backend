namespace TECHUB.Repository.Models
{
    public class FriendRequest
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime DateSent { get; set; }

        public virtual User? Sender { get; set; }
        public virtual User? Receiver { get; set; }
    }
}
