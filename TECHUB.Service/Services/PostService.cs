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
            return await repo.AddPost(post);
        }

        public async Task<Post> DeletePost(int id)
        {
            return await repo.DeletePost(id);
        }

        public Task<Post> GetPostById(int id)
        {
            return repo.GetPostById(id);
        }

        public Task<List<Post>> GetUserFeed(int id)
        {
            return repo.GetUserFeed(id);
        }

        public Task<List<Post>> GetUserFollowerFeed(int id)
        {
            var userFeed = repo.GetUserFeed(id);

            return userFeed;
        }

        public Task<List<Post>> GetUserFreindFeed(int id)
        {
            throw new NotImplementedException();
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
