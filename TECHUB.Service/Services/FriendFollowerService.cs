using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

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
    }
}
