using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IPostRepository
    {
        public Task<Post> CreatePost(Post post);
        public Task<Post> UpdatePost(Post post);
        public Task<Post> DeletePost(int id);
    }
}
