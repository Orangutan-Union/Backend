namespace TECHUB.Repository.Models
{
    public class GroupUser
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int Type { get; set; }

        public virtual User? User { get; set; }
        public virtual Group? Group { get; set; }
    }
}
