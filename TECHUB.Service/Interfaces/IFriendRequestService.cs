using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IFriendRequestService
    {
        Task<List<FriendRequest>> GetReceivedRequests(int id);
        Task<List<FriendRequest>> GetSentRequests(int id);
        Task<FriendRequest> SendFriendRequest(FriendRequest request);
        Task<bool> DeleteFriendRequest(FriendRequest request);
    }
}
