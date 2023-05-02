using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController: ControllerBase
    {
        private readonly IPostService service;

        public PostController(IPostService service) { this.service = service; }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await service.GetPostById(id);

            if (post == null)
            {
                return NotFound($"Could not find the post with ID = {id}");
            }

            return Ok(post);
        }

        [HttpGet("user/{id:int}")]
        public async Task<IActionResult> GetUserPosts(int id)
        {
            var posts = await service.GetUserPosts(id);

            if (posts == null)
            {
                return NotFound($"Could not find the posts for user with ID = {id}");
            }

            return Ok(posts);
        }

        [HttpGet("feed/{id:int}")]
        public async Task<IActionResult> GetUserFeed(int id)
        {
            var feed = await service.GetUserFeed(id);

            if (feed == null)
            {
                return NotFound($"Could not find the feed for user with ID = {id}");
            }

            return Ok(feed);
        }

        [HttpGet("followerfeed/{id:int}")]
        public async Task<IActionResult> GetUserFollowerFeed(int id)
        {
            var feed = await service.GetUserFollowerFeed(id);

            if (feed == null)
            {
                return NotFound($"Could not find the follower feed for user with ID = {id}");
            }

            return Ok(feed);
        }

        [HttpGet("friendfeed/{id:int}")]
        public async Task<IActionResult> GetUserFriendFeed(int id)
        {
            var feed = await service.GetUserFriendFeed(id);

            if (feed == null)
            {
                return NotFound($"Could not find the friend feed for user with ID = {id}");
            }

            return Ok(feed);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostViewModel post)
        {
            return Ok(await service.AddPost(post));
        }

        [HttpPost("comment")]
        public async Task<IActionResult> AddComment(Post post)
        {
            return Ok(await service.AddComment(post));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await service.DeletePost(id);

            if (post == null)
            {
                return NotFound($"Could not find the post with ID = {id}");
            }

            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            var updatePost = await service.UpdatePost(post);

            if (updatePost == null)
            {
                return NotFound($"Could not find the post");
            }

            return Ok(updatePost);
        }
    }
}
