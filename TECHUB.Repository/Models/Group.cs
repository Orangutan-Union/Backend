namespace TECHUB.Repository.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public int PictureId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public DateTime TimeCreated { get; set; }

        public virtual Picture? Picture { get; set; }
        public virtual List<Post> Posts { get; set; } = new List<Post>();
    }
}
