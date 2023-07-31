using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupUserRepository
    {
        public Task<GroupUser> AddGroupInvitation(GroupUser groupUser);
        public Task<GroupUser> DeleteGroupUser(int id);
    }
}
