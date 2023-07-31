using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly DatabaseContext context;
        public GroupUserRepository(DatabaseContext context) { this.context = context; }
        public async Task<GroupUser> AddGroupInvitation(GroupUser groupUser)
        {
            context.Add(groupUser);
            await context.SaveChangesAsync();

            return groupUser;
        }

        public async Task<GroupUser> DeleteGroupUser(int id)
        {
            var groupUser = await context.GroupUsers.FindAsync(id);

            if (groupUser != null)
            {
                context.GroupUsers.Remove(groupUser);
                await context.SaveChangesAsync();
            }

            return groupUser;
        }
    }
}
