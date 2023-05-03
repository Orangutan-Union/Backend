namespace TECHUB.Service.ViewModels
{
    public class AddPostViewModel
    {
        public int UserId { get; set; }
        public int? GroupId { get; set; } = null;
        public DateTime TimeStamp { get; set; }
        public string Content { get; set; }
        public bool FriendOnly { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //public virtual List<PicturePost> PicturePosts { get; set; } = new List<PicturePost>();
    }
}
