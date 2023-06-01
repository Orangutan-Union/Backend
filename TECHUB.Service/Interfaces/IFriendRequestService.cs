using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IFriendRequestService
    {
        Task<List<FriendRequest>> GetReceivedRequests(int id);
        Task<List<FriendRequest>> GetSentRequests(int id);
        Task<FriendRequest> SendFriendRequest(FriendRequestViewModel request);
        Task<FriendFollower> AcceptFriendRequest(FriendRequestViewModel viewmodel);
        Task<bool> DeclineFriendRequest(FriendRequestViewModel viewmodel);
    }
}
