using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class GroupRequestRepository : IGroupRequestRepository
    {
        private readonly DatabaseContext context;
        public GroupRequestRepository(DatabaseContext context) { this.context = context; }

        public async Task<List<GroupUser>> GetGroupsRequests(int id)
        {
            return await context.GroupUsers.Where(gu => gu.GroupId == id).Include(gu => gu.Group).ToListAsync();
        }

        public async Task<List<GroupUser>> GetUsersRequest(int id)
        {
            return await context.GroupUsers.Where(gu => gu.UserId == id).Include(gu => gu.User).ToListAsync();
        }

        public async Task<GroupUser> AddGroupInvitation(GroupUser groupUser)
        {
            context.Add(groupUser);
            await context.SaveChangesAsync();

            return groupUser;
        }

        public Task<List<GroupRequest>> GetGroupsJoinRequests(int groupId, int type)
        {
            throw new NotImplementedException();
        }

        public Task<List<GroupRequest>> GetUsersJoinRequests(int userId, int type)
        {
            throw new NotImplementedException();
        }
    }
}
