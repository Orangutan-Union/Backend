﻿using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class GroupRequestRepository : IGroupRequestRepository
    {
        private readonly DatabaseContext context;
        public GroupRequestRepository(DatabaseContext context) { this.context = context; }

        public async Task<GroupRequest> GetGroupJoinRequest(GroupRequest groupRequest)
        {
            var oldGroupRequest = await context.GroupRequests
                .FirstOrDefaultAsync(gr => gr.GroupId == groupRequest.GroupId & gr.UserId == groupRequest.UserId);

            return oldGroupRequest;
        }

        public async Task<List<GroupRequest>> GetGroupsJoinRequests(int id, int type)
        {
            return await context.GroupRequests
                .Where(gr => gr.GroupId == id & gr.Type == type)
                .Include(gr => gr.User).ToListAsync();
        }

        public async Task<List<GroupRequest>> GetUsersJoinRequests(int id, int type)
        {
            return await context.GroupRequests
                .Where(gr => gr.UserId == id & gr.Type == type)
                .Include(gr => gr.Group).ToListAsync();
        }

        public async Task<GroupRequest> AddGroupRequest(GroupRequest groupRequest)
        {
            context.GroupRequests.Add(groupRequest);
            await context.SaveChangesAsync();

            return groupRequest;
        }

        public async Task<GroupRequest> DeleteGroupRequest(GroupRequest group)
        {
            var groupRequest = await context.GroupRequests.FirstOrDefaultAsync(gr => gr.GroupId == group.GroupId && gr.UserId == group.UserId);
            
            if (groupRequest != null)
            {
                context.GroupRequests.Remove(group);
                await context.SaveChangesAsync();
            }
            return groupRequest;
        }
    }
}
