using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<User>> GetGroupUsers(int id);
        Task<List<Group>> GetUserGroups(int id);
        Task<Group> AddGroup(Group group);
        Task<Group> UpdateGroup(Group group);
        Task<Group> DeleteGroup(int id);
    }
}
