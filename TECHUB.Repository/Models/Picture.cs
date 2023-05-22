namespace TECHUB.Repository.Models
{
    public class Picture
    {
        public int? PictureId { get; set; }
        public string? ImageName { get; set; } = string.Empty;
        public byte[]? ImageData { get; set; }

        public virtual List<User> User { get; set; } = new List<User>();
        public virtual Group? Group { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
