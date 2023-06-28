using Microsoft.AspNetCore.Http;
using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IPostService
    {
        public Task<Post> AddPost(IFormCollection formData);
        public Task<Post> UpdatePost(Post post);
        public Task<Post> DeletePost(int id);
    }
}
