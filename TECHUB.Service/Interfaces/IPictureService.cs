using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IPictureService
    {
        Task<Picture> GetPictureById(int id);
        Task<List<Picture>> GetUserPostsPictures(int id);
        Task<Picture> AddPicture(Picture picture);
    }
}
