using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository repo;
        public GroupService(IGroupRepository repo) { this.repo = repo; }

        public async Task<Group> AddGroup(Group group)
        {
            group.TimeCreated = DateTime.Now;

            return await repo.AddGroup(group);
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
