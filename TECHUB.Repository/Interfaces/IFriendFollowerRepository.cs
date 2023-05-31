using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IFriendFollowerRepository
    {
        Task<List<FriendFollower>> GetUserFriends(int id);
        Task<List<FriendFollower>> GetUserFollowers(int id);
        Task<FriendFollower> AddFriendFollower(FriendFollower ff);
        Task<FriendFollower> UpdateFriendFollower(FriendFollower ff);
    }
}
