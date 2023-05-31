using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IFriendFollowerService
    {
        Task<List<FriendFollower>> GetUserFriends(int id);
        Task<List<FriendFollower>> GetUserFollowers(int id);
    }
}
