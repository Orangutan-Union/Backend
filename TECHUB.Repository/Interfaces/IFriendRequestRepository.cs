using TECHUB.Repository.Models;
using TECHUB.Repository.Repositories;

namespace TECHUB.Repository.Interfaces
{
    public interface IFriendRequestRepository
    {
        Task<List<FriendRequest>> GetReceivedRequests(int id);
        Task<List<FriendRequest>> GetSentRequests(int id);
        Task<FriendRequest> GetRequestById(int senderId, int receiverId);
        Task<FriendRequest> SendFriendRequest(FriendRequest request);
        Task<bool> DeleteFriendRequest(FriendRequest request);
    }
}
