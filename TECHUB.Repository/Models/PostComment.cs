namespace TECHUB.Repository.Models
{
    public class PostComment
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual Post? Comment { get; set; }
    }
}
