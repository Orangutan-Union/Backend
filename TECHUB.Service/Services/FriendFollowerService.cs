using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class FriendFollowerService : IFriendFollowerService
    {
        private readonly IFriendFollowerRepository repo;

        public FriendFollowerService(IFriendFollowerRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<FriendFollower>> GetUserFriends(int id)
        {
            return await repo.GetUserFriends(id);
        }

        public async Task<List<FriendFollower>> GetUserFollowers(int id)
        {
            return await repo.GetUserFollowers(id);
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

            return await repo.AddFriendFollower(friendFollower);
        }

        public async Task<FriendFollower> FollowUser(int userid, int targetuserid)
        {
            var friendList = await repo.GetUserFriends(userid);
            if (friendList.Any(x => x.OtherUserId == targetuserid))
            {
                return null;
            }
            var friendFollower = new FriendFollower()
            {
                UserId = userid,
                OtherUserId = targetuserid,
                Date = DateTime.Now,
                Type = 2
            };

            return await repo.AddFriendFollower(friendFollower);
        }

        public async Task<FriendFollower> BlockUser(int userid, int targetuserid)
        {
            var friendsFollowers = await repo.GetUserFriendsFollowers(userid);

            if (!await repo.FindUsers(userid, targetuserid))
            {
                return null;
            }
            // Check if there's an existing relation between the 2 users, if not then create one.
            if (!friendsFollowers.Any(x => x.UserId == userid && x.OtherUserId == targetuserid || x.UserId == targetuserid && x.OtherUserId == userid))
            {
                var ff = new FriendFollower()
                {
                    UserId = userid,
                    OtherUserId = targetuserid,
                    Date= DateTime.Now,
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
