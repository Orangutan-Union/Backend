using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IGroupRequestService
    {
        public Task<List<GroupRequest>> GetGroupsJoinRequests(int groupId, int type);
        public Task<List<GroupRequest>> GetUsersJoinRequests(int userId, int type);
        public Task<GroupRequest> GetGroupJoinRequest(GroupRequest groupRequest);
        public Task<GroupRequest> AddGroupRequest(GroupRequest group);
        public Task<GroupRequest> AcceptGroupRequest(GroupRequest group);
        public Task<GroupRequest> DeleteGroupRequest(GroupRequest group);
    }
}
