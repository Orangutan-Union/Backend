using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IPictureRepository
    {
        Task<Picture> GetPictureById(int id);
        Task<Picture> Add(Picture picture);
        Task<Picture> DeletePicture(int id);
    }
}
