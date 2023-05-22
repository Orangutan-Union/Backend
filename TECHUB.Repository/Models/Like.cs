namespace TECHUB.Repository.Models
{
    public class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int? PostId { get; set; } = null;
        public int? CommentId { get; set; } = null;
        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Comment? Comment { get; set; }

    }
}
