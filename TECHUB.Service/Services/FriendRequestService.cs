using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestRepository repo;

        public FriendRequestService(IFriendRequestRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<FriendRequest>> GetReceivedRequests(int id)
        {
            return await repo.GetReceivedRequests(id);
        }

        public async Task<List<FriendRequest>> GetSentRequests(int id)
        {
            return await repo.GetSentRequests(id);
        }

        public async Task<FriendRequest> SendFriendRequest(FriendRequest request)
        {
            request.DateSent = DateTime.Now;
            return await repo.SendFriendRequest(request);
        }

        public async Task<bool> DeleteFriendRequest(FriendRequest request)
        {
            return await repo.DeleteFriendRequest(request);
        }
    }
}
