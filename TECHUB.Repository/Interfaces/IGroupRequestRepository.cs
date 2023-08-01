using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRequestRepository
    {
        public Task<GroupRequest> GetGroupJoinRequest(GroupRequest groupRequest);
        public Task<List<GroupRequest>> GetGroupsJoinRequests(int id, int type);
        public Task<List<GroupRequest>> GetUsersJoinRequests(int id, int type);
        public Task<GroupRequest> AddGroupRequest(GroupRequest group);
        public Task<GroupRequest> DeleteGroupRequest(GroupRequest group);
    }
}
