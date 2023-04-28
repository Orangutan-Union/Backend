using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRepository
    {
        public Task<Group> GetGroup(int id);
        public Task<List<Group>> GetUserGroups(int id);
        public Task<Group> AddGroup(Group group);
        public Task<Group> UpdateGroup(Group group);
        public Task<Group> DeleteGroup(int id);
    }
}
