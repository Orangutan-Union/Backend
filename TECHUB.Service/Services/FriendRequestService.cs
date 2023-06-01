using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

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

        public async Task<FriendRequest> SendFriendRequest(FriendRequestViewModel viewmodel)
        {
            var friendRequest = new FriendRequest()
            {
                SenderId = viewmodel.SenderId,
                ReceiverId = viewmodel.ReceiverId,
                DateSent = DateTime.Now,
            };
            return await repo.SendFriendRequest(friendRequest);
        }

        public async Task<bool> DeleteFriendRequest(FriendRequestViewModel viewmodel)
        {
            var friendRequest = await repo.GetRequestById(viewmodel.SenderId, viewmodel.ReceiverId);

            if (friendRequest is null)
            {
                return false;
            }

            return await repo.DeleteFriendRequest(friendRequest);
        }
    }
}
