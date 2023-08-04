using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupUserRepository
    {
        Task<GroupUser> GetGroupUser(int userId, int groupId);
        Task<GroupUser> AddGroupUser(GroupUser groupUser);
        Task<GroupUser> UpdateGroupUser(GroupUser groupUser);
        Task<GroupUser> DeleteGroupUser(int userId, int groupId);
    }
}
