namespace TECHUB.Repository.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
        public virtual List<Like> Likes { get; set; } = new List<Like>();
    }
}
