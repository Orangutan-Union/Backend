namespace TECHUB.Repository.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        //public int ProfilePictureId { get; set; }
        //public int UserId { get; set; }
        public string Path { get; set; } = string.Empty;

        public virtual User? User { get; set; }
        public virtual Group? Group { get; set; }
        public virtual List<PicturePost> PicturePosts { get; set; } = new List<PicturePost>();
    }
}
