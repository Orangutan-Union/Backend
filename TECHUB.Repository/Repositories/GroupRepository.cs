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

        public async Task<Group> GetGroup(int id)
        {
            return await context.Groups
                .Include(g => g.Picture)

                .Include(g => g.GroupUsers)

                .Include(g => g.Posts)
                .ThenInclude(p => p.Likes)

                .Include(g => g.Posts)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.Picture)

                .Include(g => g.Posts)
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)

                .Include(g => g.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)

                .SingleOrDefaultAsync(g => g.GroupId == id);
        }

        public async Task<List<Group>> GetUserGroups(int id)
        {
            var user = await context.Users.Include(u => u.GroupUsers).ThenInclude(gu => gu.Group).FirstOrDefaultAsync(u => u.UserId == id);
            var list = new List<Group>();
            foreach (var item in user.GroupUsers)
            {
                list.Add(item.Group);
            }

            return list;
        }

        public async Task<Group> UpdateGroup(Group group)
        {
            context.Entry(group).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return group;
        }
    }
}
