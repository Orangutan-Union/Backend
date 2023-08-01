using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IGroupUserService
    {
        public Task<GroupUser> GetGroupUser(int userId, int groupId);
        public Task<GroupUser> AddGroupUser(GroupUser groupUser);
        public Task<GroupUser> UpdateGroupUser(GroupUser groupUser);
        public Task<GroupUser> DeleteGroupUser(int id);
    }
}
