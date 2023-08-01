using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> AddGroup(Group group);
        Task<Group> UpdateGroup(Group group);
        Task<Group> DeleteGroup(int id);
    }
}
