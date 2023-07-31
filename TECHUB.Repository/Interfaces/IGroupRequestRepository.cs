using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRequestRepository
    {
        public Task<List<GroupRequest>> GetGroupsJoinRequests(int groupId, int type);
        public Task<List<GroupRequest>> GetUsersJoinRequests(int userId, int type);
    }
}
