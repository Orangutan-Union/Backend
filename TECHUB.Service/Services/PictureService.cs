using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class PictureService : IPictureService
    {
        private readonly IPictureRepository repo;

        public PictureService(IPictureRepository repo)
        {
            this.repo = repo;
        }

        public async Task<Picture> GetPictureById(int id)
        {
            return await repo.GetPictureById(id);
        }

        public async Task<Picture> AddPicture(Picture picture)
        {
            var pic = await repo.Add(picture);
            return pic;
        }

        public async Task<Picture> DeletePicture(int id)
        {
            var pic = await repo.DeletePicture(id);

            return pic;
        }
    }
}
