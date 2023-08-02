using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IFriendFollowerRepository
    {
        Task<List<FriendFollower>> GetUserFriends(int id);
        Task<List<FriendFollower>> GetUserFollowing(int id);
        Task<List<FriendFollower>> GetUserFollowers(int id);
        Task<List<FriendFollower>> GetBlockedUsers(int id);
        Task<List<FriendFollower>> GetBlockingUsers(int id);
        Task<List<FriendFollower>> GetUserFriendsFollowers(int id);
        Task<FriendFollower> AddFriendFollower(FriendFollower ff);
        Task<FriendFollower> UpdateFriendFollower(FriendFollower ff);
        Task<bool> DeleteFriendFollower(FriendFollower ff);
        Task<bool> FindUsers(int userid, int targetid);
    }
}
