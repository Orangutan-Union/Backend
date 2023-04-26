namespace TECHUB.Repository.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool FriendOnly { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public List<Post> Comments { get; set; } = new List<Post>();

        public virtual User? User { get; set; }
        public virtual Group? Group { get; set; }
        public virtual List<Picture> Pictures { get; set; } = new List<Picture>();
        public virtual List<Like> Likes { get; set; } = new List<Like>();

    }
}
