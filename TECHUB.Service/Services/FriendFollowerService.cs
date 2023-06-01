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

            var friendFollower = new FriendFollower()
            {
                UserId = request.SenderId,
                OtherUserId = request.ReceiverId,
                Date = DateTime.Now,
                Type = 1
            };

            return await repo.AddFriendFollower(friendFollower);
        }
    }
}
