using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IFriendFollowerService
    {
        Task<List<FriendFollower>> GetUserFriends(int id);
        Task<List<FriendFollower>> GetUserFollowers(int id);
        Task<FriendFollower> AddFriend(FriendRequestViewModel request);
        Task<FriendFollower> FollowUser(int userid, int targetuserid);
        Task<FriendFollower> BlockUser(int userid, int targetuserid);
        Task<bool> UnblockUser(int userid, int targetuserid);
        Task<bool> UnfollowUser(int userid, int targetuserid);
    }
}
