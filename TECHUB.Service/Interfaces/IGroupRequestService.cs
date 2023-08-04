using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IGroupRequestService
    {
        public Task<List<GroupRequest>> GetGroupsJoinRequests(int groupId, int type);
        public Task<List<GroupRequest>> GetUsersJoinRequests(int userId, int type);
        public Task<GroupRequest> GetGroupJoinRequest(int userId, int groupId);
        public Task<GroupRequest> AddGroupRequest(GroupRequest groupRequest);
        public Task<GroupRequest> AcceptGroupRequest(GroupRequest groupRequest);
        public Task<GroupRequest> DeleteGroupRequest(int userId, int groupId);
    }
}
