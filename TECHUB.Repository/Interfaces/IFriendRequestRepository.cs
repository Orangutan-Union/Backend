using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IFriendRequestRepository
    {
        Task<List<FriendRequest>> GetAllRequests(int id);
        Task<List<FriendRequest>> GetReceivedRequests(int id);
        Task<List<FriendRequest>> GetSentRequests(int id);
        Task<FriendRequest> GetRequestById(int senderId, int receiverId);
        Task<FriendRequest> SendFriendRequest(FriendRequest request);
        Task<bool> DeleteFriendRequest(int senderId, int receiverId);
    }
}
