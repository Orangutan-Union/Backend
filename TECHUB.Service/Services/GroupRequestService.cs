using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class GroupRequestService : IGroupRequestService
    {
        private readonly IGroupRequestRepository repo;
        private readonly IGroupUserService groupUserService;
        public GroupRequestService(IGroupRequestRepository repo) { this.repo = repo; }

        public async Task<GroupRequest> AcceptGroupRequest(GroupRequest groupRequest)
        {
            var oldGroupRequest = await repo.GetGroupJoinRequest(groupRequest);

            if (oldGroupRequest == null)
            {
                return null;
            }
            else
            {
                GroupUser groupUser = new GroupUser();
                groupUser.GroupId = groupRequest.GroupId;
                groupUser.UserId = groupRequest.UserId;
                groupUser.Type = groupRequest.Type;

                await groupUserService.AddGroupUser(groupUser);
                return await repo.DeleteGroupRequest(groupRequest);
            }
        }

        public async Task<GroupRequest> AddGroupRequest(GroupRequest groupRequest)
        {
            var oldGroupRequest = await repo.GetGroupJoinRequest(groupRequest);

            if (oldGroupRequest == null)
            {
                return await repo.AddGroupRequest(groupRequest);
            }
            else if (groupRequest.UserId == oldGroupRequest.UserId & groupRequest.GroupId == oldGroupRequest.GroupId &
                groupRequest.Type != groupRequest.Type)
            {
                return await AcceptGroupRequest(oldGroupRequest);
            }
            else
            {
                return null;
            }
        }

        public async Task<GroupRequest> DeleteGroupRequest(GroupRequest groupRequest)
        {
            return await repo.DeleteGroupRequest(groupRequest);
        }

        public async Task<GroupRequest> GetGroupJoinRequest(GroupRequest groupRequest)
        {
            return await repo.GetGroupJoinRequest(groupRequest);
        }

        public async Task<List<GroupRequest>> GetGroupsJoinRequests(int groupId, int type)
        {
            return await repo.GetGroupsJoinRequests(groupId, type);
        }

        public async Task<List<GroupRequest>> GetUsersJoinRequests(int userId, int type)
        {
            return await repo.GetUsersJoinRequests(userId, type);
        }
    }
}
