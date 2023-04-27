namespace TECHUB.Repository.Models
{
    public class Like
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
