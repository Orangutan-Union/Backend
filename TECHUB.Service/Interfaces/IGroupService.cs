using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IGroupService
    {
        Task<Group> AddGroup(Group group, int id);
        Task<Group> DeleteGroup(int Id);
        Task<Group> UpdateGroup(Group group);
    }
}
