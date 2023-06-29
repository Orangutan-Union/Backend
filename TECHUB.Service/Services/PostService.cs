using Microsoft.AspNetCore.Http;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class PostService: IPostService
    {
        private readonly IPostRepository repo;
        private readonly IPhotoService photoService;
        public PostService(IPostRepository repo, IPhotoService photoService) 
        {
            this.repo = repo; 
            this.photoService = photoService;
        }

        public async Task<Post> AddPost(IFormCollection formData)
        {
            Post newPost = new Post();
            newPost.UserId = Convert.ToInt32(formData["user_id"][0]);
            newPost.TimeStamp = DateTime.Now;
            newPost.Content = formData["content"][0]!;
            newPost.Latitude = Convert.ToInt32(formData["latitude"][0]);
            newPost.Longitude = Convert.ToInt32(formData["longitude"][0]);

            // Check if there's a group ID set, set null if not
            if (formData["group_id"][0] != "")
            {
                newPost.GroupId = Convert.ToInt32(formData["group_id"][0]);
            }
            else
            {
                newPost.GroupId = null;
            }

            // Check if there are any files attached
            if (formData.Files.Count == 0)
            {
                return await repo.CreatePost(newPost);
            }

            // Try to upload image to file-server, if it fails don't create post.
            var result = await photoService.UploadPhotoAsync(formData.Files[0]);
            if (result.Error is not null)
            {
                return null!;
            }

            Picture postPic = new Picture()
            {
                ImageName = formData.Files[0].FileName,
                ImageUrl = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };
            newPost.Pictures.Add(postPic);

            return await repo.CreatePost(newPost);
        }

        public async Task<Post> DeletePost(int id)
        {
            return await repo.DeletePost(id);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            return await repo.UpdatePost(post);
        }
    }
}
