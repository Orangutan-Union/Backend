using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestRepository repo;
        private readonly IFriendFollowerService ffService;

        public FriendRequestService(IFriendRequestRepository repo, IFriendFollowerService ffService)
        {
            this.repo = repo;
            this.ffService = ffService;
        }

        public async Task<List<FriendRequest>> GetAllRequests(int id)
        {
            return await repo.GetAllRequests(id);
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
            var friendList = await ffService.GetUserFriends(viewmodel.SenderId);
            var ff = friendList.Any(x => x.OtherUserId == viewmodel.ReceiverId);

            if (ff)
            {
                return null;
            }

            var friendRequest = new FriendRequest()
            {
                SenderId = viewmodel.SenderId,
                ReceiverId = viewmodel.ReceiverId,
                DateSent = DateTime.Now,
            };
            return await repo.SendFriendRequest(friendRequest);
        }

        public async Task<FriendFollower> AcceptFriendRequest(FriendRequestViewModel viewmodel)
        {
            var res = await repo.DeleteFriendRequest(viewmodel.SenderId, viewmodel.ReceiverId);
            if (!res)
            {
                return null;
            }
            return await ffService.AddFriend(viewmodel);            
        }

        public async Task<bool> DeclineFriendRequest(FriendRequestViewModel viewmodel)
        {
            var res = await repo.DeleteFriendRequest(viewmodel.SenderId, viewmodel.ReceiverId);
            return res;
        }
    }
}
