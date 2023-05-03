using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IPictureService
    {
        Task<Picture> GetPictureById(int id);
    }
}
