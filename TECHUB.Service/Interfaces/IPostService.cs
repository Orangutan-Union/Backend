﻿using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IPostService
    {
        public Task<Post> GetPostById(int id);
        public Task<List<Post>> GetUserPosts(int id);
        public Task<List<Post>> GetUserFeed(int id);
        public Task<List<Post>> GetUserFollowerFeed(int id);
        public Task<List<Post>> GetUserFreindFeed(int id);
        public Task<Post> AddPost(Post post);
        public Task<Post> UpdatePost(Post post);
        public Task<Post> DeletePost(int id);
    }
}