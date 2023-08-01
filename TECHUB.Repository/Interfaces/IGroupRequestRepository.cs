using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRequestRepository
    {
        Task<GroupRequest> GetGroupJoinRequest(GroupRequest groupRequest);
        Task<List<GroupRequest>> GetGroupsJoinRequests(int id, int type);
        Task<List<GroupRequest>> GetUsersJoinRequests(int id, int type);
        Task<GroupRequest> AddGroupRequest(GroupRequest group);
        Task<GroupRequest> DeleteGroupRequest(GroupRequest group);
    }
}
