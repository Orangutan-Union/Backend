namespace TECHUB.Repository.Models
{
    public class PicturePost
    {
        public int PictureId { get; set; }
        public int PostId { get; set; }

        public virtual Picture? Picture { get; set; }
        public virtual Post? Post { get; set; }
    }
}
