using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IPostService
    {
        public Task<Post> GetPostById(int id);
        public Task<List<Post>> GetUserPosts(int id);
        public Task<List<Post>> GetUserFeed(int id);
        public Task<List<Post>> GetUserFollowerFeed(int id);
        public Task<List<Post>> GetUserFriendFeed(int id);
        public Task<Post> AddPost(AddPostViewModel post);
        public Task<Post> AddComment(Post post);
        public Task<Post> UpdatePost(Post post);
        public Task<Post> DeletePost(int id);
    }
}
