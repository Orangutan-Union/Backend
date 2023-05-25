namespace TECHUB.Repository.Models
{
    public class Picture
    {
        public int? PictureId { get; set; }
        public string? PublicId { get; set; } = string.Empty;
        public string? ImageName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;

        public virtual List<User> User { get; set; } = new List<User>();
        public virtual Group? Group { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
