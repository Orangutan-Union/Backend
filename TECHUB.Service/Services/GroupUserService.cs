using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class GroupUserService : IGroupUserService
    {
        private readonly IGroupUserRepository repo;
        public GroupUserService(IGroupUserRepository repo) { this.repo = repo; }

        public async Task<GroupUser> AddGroupUser(GroupUser groupUser)
        {
            return await repo.AddGroupUser(groupUser);
        }

        public async Task<GroupUser> DeleteGroupUser(int id)
        {
            return await repo.DeleteGroupUser(id);
        }

        public async Task<GroupUser> GetGroupUser(int userId, int groupId)
        {
            return await repo.GetGroupUser(userId, groupId);
        }

        public async Task<GroupUser> UpdateGroupUser(GroupUser groupUser)
        {
            return await repo.UpdateGroupUser(groupUser);
        }
    }
}
