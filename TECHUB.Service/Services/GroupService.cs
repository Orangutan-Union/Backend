using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository repo;
        private readonly IGroupUserRepository groupUserRepo;
        private readonly IPictureRepository pictureRepo;
        public GroupService(IGroupRepository repo, IPictureRepository pictureRepository, IGroupUserRepository groupUserRepo) 
        { this.repo = repo; this.pictureRepo = pictureRepository; this.groupUserRepo = groupUserRepo; }

        public async Task<Group> AddGroup(Group group, int id)
        {
            group.TimeCreated = DateTime.Now;            

            var existingPic = await pictureRepo.GetPictureById(1);
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

            Group newGroup = await repo.AddGroup(group);

            GroupUser groupUser = new GroupUser();
            groupUser.UserId = id;
            groupUser.GroupId = id;
            groupUser.Type = 1;

            await groupUserRepo.AddGroupUser(groupUser);

            return newGroup;
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
