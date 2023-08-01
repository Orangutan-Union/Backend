using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository repo;
        private readonly IPictureRepository pictureRepository;
        public GroupService(IGroupRepository repo, IPictureRepository pictureRepository) { this.repo = repo; this.pictureRepository = pictureRepository; }

        public async Task<Group> AddGroup(Group group)
        {
            group.TimeCreated = DateTime.Now;
            var existingPic = await pictureRepository.GetPictureById(1);
            if (existingPic is null)
            {
                var newPic = new Picture()
                {
                    ImageName = "default",
                    ImageUrl = "https://res.cloudinary.com/dm52kqhd4/image/upload/v1685013104/gjemhamzrvs8kxgj113l.png",
                };
                group.Picture = newPic;
            }
            else
            {
                group.Picture = existingPic;
            }
            return await repo.AddGroup(group);
        }

        public async Task<Group> DeleteGroup(int Id)
        {
            return await repo.DeleteGroup(Id);
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            return await repo.UpdateGroup(group);
        }
    }
}
