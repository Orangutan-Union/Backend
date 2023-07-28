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

        public async Task<Group> UpdateGroup(Group group)
        {
            context.Entry(group).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return group;
        }
    }
}
