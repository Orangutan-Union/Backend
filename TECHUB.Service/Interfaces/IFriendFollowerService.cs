using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IFriendFollowerService
    {
        Task<List<FriendFollower>> GetUserFriends(int id);
        Task<List<FriendFollower>> GetUserFollower(int id);
        Task<List<FriendFollower>> GetUserFollowing(int id);
        Task<List<FriendFollower>> GetBlockedUsers(int id);
        Task<List<FriendFollower>> GetBlockingUsers(int id);
        Task<FriendFollower> AddFriend(FriendRequestViewModel request);
        Task<FriendFollower> FollowUser(FriendFollower ff);
        Task<FriendFollower> BlockUser(int userid, int targetuserid);
        Task<bool> UnblockUser(int userid, int targetuserid);
        Task<bool> UnfollowUser(int userid, int targetuserid);
        Task<bool> RemoveFriend(int userid, int targetuserid);
    }
}
