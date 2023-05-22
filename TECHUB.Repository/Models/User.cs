namespace TECHUB.Repository.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int ProfilePictureId { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public virtual Picture? Picture { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
        public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        public virtual List<Like> Likes { get; set; } = new List<Like>();
        //public virtual List<Picture> Pictures { get; set; } = new List<Picture>();
        public virtual List<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual List<FriendRequest> ReceivedFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual List<FriendFollower> UserFriendFollowers { get; set; } = new List<FriendFollower>();
        public virtual List<FriendFollower> OtherUserFriendFollowers { get; set; } = new List<FriendFollower>();
        public virtual List<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
        public virtual List<Chat> Chats { get; set; } = new List<Chat>();
        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
