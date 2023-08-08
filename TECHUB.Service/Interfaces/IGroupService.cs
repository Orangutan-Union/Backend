using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IGroupService
    {
        Task<List<User>> GetGroupUsers(int id);
        Task<List<Group>> GetUserGroups(int id);
        Task<Group> AddGroup(Group group, int id);
        Task<Group> DeleteGroup(int Id);
        Task<Group> UpdateGroup(Group group);
    }
}
