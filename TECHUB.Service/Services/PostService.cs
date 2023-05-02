using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class PostService: IPostService
    {
        private readonly IPostRepository repo;
        public PostService(IPostRepository repo) { this.repo = repo; }

        public async Task<Post> AddPost(Post post)
        {
            post.TimeStamp = DateTime.Now;

            return await repo.AddPost(post);
        }

        public async Task<Post> DeletePost(int id)
        {
            return await repo.DeletePost(id);
        }

        public async Task<Post> GetPostById(int id)
        {
            return await repo.GetPostById(id);
        }

        public async Task<List<Post>> GetUserFeed(int id)
        {
            return await repo.GetUserFeed(id);
        }

        public async Task<List<Post>> GetUserFollowerFeed(int id)
        {
            return await repo.GetUserFollowerFeed(id);
        }

        public async Task<List<Post>> GetUserFreindFeed(int id)
        {
            return await repo.GetUserFriendFeed(id);
        }

        public Task<List<Post>> GetUserPosts(int id)
        {
            return repo.GetUserPosts(id);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            return await repo.UpdatePost(post);
        }
    }
}
