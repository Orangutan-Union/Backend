using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace TECHUB.Repository.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        //public int UserId { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public byte[] ImageData { get; set; }

        public virtual User? User { get; set; }
        public virtual Group? Group { get; set; }
        public virtual List<PicturePost> PicturePosts { get; set; } = new List<PicturePost>();
    }
}
