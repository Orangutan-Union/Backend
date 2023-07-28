using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly DatabaseContext context;

        public FriendRequestRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<FriendRequest>> GetAllRequests(int id)
        {
            return await context.FriendRequests
                .Include(x => x.Sender)
                .ThenInclude(x => x!.Picture)
                .Include(x => x.Receiver)
                .ThenInclude(s => s!.Picture)
                .Where(x => x.ReceiverId == id || x.SenderId == id).OrderByDescending(x => x.DateSent).ToListAsync();
        }

        public async Task<List<FriendRequest>> GetReceivedRequests(int id)
        {
            return await context.FriendRequests
                .Include(x => x.Sender)
                .ThenInclude(s => s!.Picture)
                .Where(x => x.ReceiverId == id).OrderByDescending(x => x.DateSent).ToListAsync();
        }

        public async Task<List<FriendRequest>> GetSentRequests(int id)
        {
            return await context.FriendRequests
                .Include(x => x.Receiver)
                .ThenInclude(r => r!.Picture)
                .Where(x => x.SenderId == id).OrderByDescending(x => x.DateSent).ToListAsync();
        }

        public async Task<FriendRequest> GetRequestById(int senderId, int receiverId)
        {
            return await context.FriendRequests.FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId);
        }

        public async Task<FriendRequest> SendFriendRequest(FriendRequest request)
        {
            context.FriendRequests.Add(request);
            await context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteFriendRequest(int senderId, int receiverId)
        {
            var fr = await context.FriendRequests.FindAsync(senderId, receiverId);
            if (fr is null)
            {
                return false;
            }

            context.FriendRequests.Remove(fr);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
