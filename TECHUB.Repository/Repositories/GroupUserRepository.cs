using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly DatabaseContext context;
        public GroupUserRepository(DatabaseContext context) { this.context = context; }

        public async Task<GroupUser> AddGroupUser(GroupUser groupUser)
        {
            groupUser.Type = 3;
            context.GroupUsers.Add(groupUser);
            await context.SaveChangesAsync();

            return groupUser;

        }

        public async Task<GroupUser> DeleteGroupUser(int userId, int groupId)
        {
            var groupUser = await context.GroupUsers.FirstOrDefaultAsync(gu => gu.UserId == userId && gu.GroupId == groupId);

            if (groupUser != null)
            {
                context.GroupUsers.Remove(groupUser);
                await context.SaveChangesAsync();
            }

            return groupUser;
        }

        public async Task<GroupUser> GetGroupUser(int userId, int groupId)
        {
            return await context.GroupUsers.FirstOrDefaultAsync(gu => gu.UserId == userId & gu.GroupId == groupId);
        }

        public async Task<GroupUser> UpdateGroupUser(GroupUser groupUser)
        {
            context.Entry(groupUser).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return groupUser;
        }
    }
}
