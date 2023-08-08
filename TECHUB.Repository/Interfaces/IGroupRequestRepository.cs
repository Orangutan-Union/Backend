using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IGroupRequestRepository
    {
        Task<GroupRequest> GetGroupJoinRequest(int userId, int groupId);
        Task<List<GroupRequest>> GetGroupsJoinRequests(int id, int type);
        Task<List<GroupRequest>> GetUsersJoinRequests(int id, int type);
        Task<GroupRequest> AddGroupRequest(GroupRequest groupRequest);
        Task<GroupRequest> DeleteGroupRequest(int userId, int groupId);
    }
}
