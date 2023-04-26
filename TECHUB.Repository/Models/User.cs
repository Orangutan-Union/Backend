namespace TECHUB.Repository.Models
{
    public class User
    {
        public int UserId { get; set; }
        public int PictureId { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public virtual Picture? Picture { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
        public virtual List<Like> Likes { get; set; } = new List<Like>();
        public virtual List<FriendRequest> FriendRequests { get; set; } = new List<FriendRequest>();
        public virtual List<FriendFollower> FriendFollowers { get; set; } = new List<FriendFollower>();

    }
}
