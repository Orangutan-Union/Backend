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

        public async Task<List<Picture>> GetUserPostsPictures(int id)
        {
            var posts = await repo.GetUserPostsPictures(id);
            var files = new List<Picture>();

            foreach (var post in posts)
            {
                files.Add(post.Pictures[0]);
            }

            return files;
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
