namespace TECHUB.Repository.Models
{
    public class GroupRequest
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public int RequestType { get; set; }

        public virtual User? User { get; set; }
        public virtual Group? Group { get; set; }
    }
}
