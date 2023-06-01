using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IFriendFollowerService
    {
        Task<List<FriendFollower>> GetUserFriends(int id);
        Task<List<FriendFollower>> GetUserFollowers(int id);
        Task<FriendFollower> AddFriend(FriendRequestViewModel request);
    }
}
