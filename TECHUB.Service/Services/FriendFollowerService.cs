using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class FriendFollowerService : IFriendFollowerService
    {
        private readonly IChatService chatService;
        private readonly IFriendFollowerRepository repo;
        private readonly IFriendRequestRepository requestRepository;

        public FriendFollowerService(IFriendFollowerRepository repo, IFriendRequestRepository requestRepository, IChatService chatService)
        {
            this.chatService = chatService;
            this.repo = repo;
            this.requestRepository = requestRepository;
        }

        public async Task<List<FriendFollower>> GetUserFriends(int id)
        {
            return await repo.GetUserFriends(id);
        }
        public async Task<List<FriendFollower>> GetUserFollowers(int id)
        {
            return await repo.GetUserFollowers(id);
        }

        public async Task<List<FriendFollower>> GetUserFollowing(int id)
        {
            return await repo.GetUserFollowing(id);
        }

        public async Task<List<FriendFollower>> GetBlockedUsers(int id)
        {
            return await repo.GetBlockedUsers(id);
        }

        public async Task<List<FriendFollower>> GetBlockingUsers(int id)
        {
            return await repo.GetBlockingUsers(id);
        }

        public async Task<FriendFollower> GetBlockedUserChat(int userId, int otherUserId)
        {
            return await repo.GetBlockedUserChat(userId, otherUserId);
        }

        public async Task<FriendFollower> AddFriend(FriendRequestViewModel request)
        {
            if (request is null)
            {
                return null;
            }

            var friendsFollowers = await repo.GetUserFriendsFollowers(request.SenderId);

            // Adds FriendFollower object(s) to a list for deletion if the 2 users are following each other or either of them follows the other.
            var filteredList = friendsFollowers
                .Where(x => x.UserId == request.SenderId && x.OtherUserId == request.ReceiverId && x.Type == 2
                || x.UserId == request.ReceiverId && x.OtherUserId == request.SenderId && x.Type == 2).ToList();

            if (filteredList.Count > 0)
            {
                foreach (var item in filteredList)
                {
                    await repo.DeleteFriendFollower(item);
                }
            }

            var friendFollower = new FriendFollower()
            {
                UserId = request.SenderId,
                OtherUserId = request.ReceiverId,
                Date = DateTime.Now,
                Type = 1
            };

            var oldChat = await chatService.GetChatByUsers(request.SenderId, request.ReceiverId);
            if (oldChat == null)
            {
                await chatService.CreatePrivateChat(request.SenderId, request.ReceiverId);
            }

            return await repo.AddFriendFollower(friendFollower);
        }

        public async Task<bool> RemoveFriend(int userid, int targetuserid)
        {
            if (!await repo.FindUsers(userid, targetuserid))
            {
                return false;
            }

            var friendList = await repo.GetUserFriendsFollowers(userid);

            var ff = friendList.FirstOrDefault(x => x.UserId == userid && x.OtherUserId == targetuserid && x.Type == 1 || x.UserId == targetuserid && x.OtherUserId == userid && x.Type == 1);

            if (ff is null)
            {
                return false;
            }
            return await repo.DeleteFriendFollower(ff);
        }

        public async Task<FriendFollower> FollowUser(FriendFollower ff)
        {
            var friendList = await repo.GetUserFriends(ff.UserId);
            if (friendList.Any(x => x.OtherUserId == ff.OtherUserId))
            {
                return null;
            }

            ff.Type = 2;
            ff.Date = DateTime.Now;
            ff.User = null;

            return await repo.AddFriendFollower(ff);
        }

        public async Task<FriendFollower> BlockUser(int userid, int targetuserid)
        {
            var friendsFollowers = await repo.GetUserFriendsFollowers(userid);

            if (!await repo.FindUsers(userid, targetuserid))
            {
                return null;
            }

            // Delete FriendRequest between the 2 users if there is one.
            var tt = await requestRepository.GetAllRequests(userid);
            var friendRequest = tt.FirstOrDefault(x => x.SenderId == targetuserid || x.ReceiverId == targetuserid);
            if (friendRequest is not null)
            {
                await requestRepository.DeleteFriendRequest(friendRequest.SenderId, friendRequest.ReceiverId);
            }

            // Check if there's an existing relation between the 2 users, if not then create one.
            if (!friendsFollowers.Any(x => x.UserId == userid && x.OtherUserId == targetuserid || x.UserId == targetuserid && x.OtherUserId == userid))
            {
                var ff = new FriendFollower()
                {
                    UserId = userid,
                    OtherUserId = targetuserid,
                    Date = DateTime.Now,
                    Type = 3
                };

                return await repo.AddFriendFollower(ff);
            }
            var oldFf = friendsFollowers.FirstOrDefault(x => x.UserId == userid && x.OtherUserId == targetuserid || x.UserId == targetuserid && x.OtherUserId == userid);
            if (oldFf is null)
            {
                return null;
            }
            oldFf.Type = 3;
            return await repo.UpdateFriendFollower(oldFf);
        }

        public async Task<bool> UnblockUser(int userid, int targetuserid)
        {
            if (!await repo.FindUsers(userid, targetuserid))
            {
                return false;
            }

            var friendsFollowers = await repo.GetUserFriendsFollowers(userid);
            var ff = friendsFollowers.FirstOrDefault(x => x.UserId == userid && x.Type == 3 && x.OtherUserId == targetuserid);
            if (ff is null)
            {
                return false;
            }
            return await repo.DeleteFriendFollower(ff);
        }

        public async Task<bool> UnfollowUser(int userid, int targetuserid)
        {
            if (!await repo.FindUsers(userid, targetuserid))
            {
                return false;
            }

            var friendsFollowers = await repo.GetUserFriendsFollowers(userid);
            var ff = friendsFollowers.FirstOrDefault(x => x.UserId == userid && x.Type == 2 && x.OtherUserId == targetuserid);
            if (ff is null)
            {
                return false;
            }
            return await repo.DeleteFriendFollower(ff);
        }
    }
}
