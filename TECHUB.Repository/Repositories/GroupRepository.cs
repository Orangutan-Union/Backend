using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace TECHUB.Repository.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DatabaseContext context;
        public GroupRepository(DatabaseContext context) { this.context = context; }

        public async Task<Group> AddGroup(Group group)
        {
            context.Groups.Add(group);
            await context.SaveChangesAsync();

            return group;
        }

        public async Task<Group> DeleteGroup(int id)
        {
            var group = await context.Groups.FindAsync(id);

            if (group != null)
            {
                context.Groups.Remove(group);
                await context.SaveChangesAsync();
            }

            return group;
        }

        public async Task<List<User>> GetGroupUsers(int id)
        {
            List<User> users = new List<User>();

            var groupUsers = await context.GroupUsers
                .Where(gu => gu.GroupId == id)
                .ToListAsync();

            foreach (var groupUser in groupUsers)
            {
                var user = await context.Users
                .Include(u => u.GroupUsers)
                .Include(u => u.Picture)
                .FirstOrDefaultAsync(u => u.UserId == groupUser.UserId);

                users.Add(user);
            }


            return users;
        }

        public async Task<List<Group>> GetUserGroups(int id)
        {
            List<Group> groups = new List<Group>();

            var groupUsers = await context.GroupUsers
                .Where(gu => gu.UserId == id)
                .ToListAsync();

            foreach (var groupUser in groupUsers)
            {
                var group = await context.Groups
                .Include(g => g.GroupUsers)
                .Include(g => g.Picture)
                .FirstOrDefaultAsync(g => g.GroupId == groupUser.GroupId);

                groups.Add(group);
            }


            return groups;
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            context.Entry(group).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return group;
        }
    }
}
