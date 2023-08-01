using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task<Post> DeletePost(int id);
    }
}
