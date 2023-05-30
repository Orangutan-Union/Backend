﻿using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class PostService: IPostService
    {
        private readonly IPostRepository repo;
        public PostService(IPostRepository repo) { this.repo = repo; }

        public async Task<Post> AddPost(AddPostViewModel post)
        {
            Post newPost = new Post();
            newPost.UserId = post.UserId;
            newPost.GroupId = post.GroupId;
            newPost.TimeStamp = DateTime.Now;
            newPost.Content = post.Content;
            newPost.FriendOnly = post.FriendOnly;
            newPost.Latitude = post.Latitude;
            newPost.Longitude = post.Longitude;

            return await repo.AddPost(newPost);
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
