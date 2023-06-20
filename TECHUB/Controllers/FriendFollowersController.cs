using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendFollowersController : ControllerBase
    {
        private readonly IFriendFollowerService service;

        public FriendFollowersController(IFriendFollowerService service)
        {
            this.service = service;
        }

        [HttpGet("{id:int}/friends")]
        public async Task<IActionResult> GetUserFriends(int id)
        {
            return Ok(await service.GetUserFriends(id));
        }

        [HttpGet("{id:int}/followers")]
        public async Task<IActionResult> GetUserFollowers(int id)
        {
            return Ok(await service.GetUserFollowers(id));
        }

        [HttpGet("{id:int}/blocked")]
        public async Task<IActionResult> GetBlockedUsers(int id)
        {
            return Ok(await service.GetBlockedUsers(id));
        }

        [HttpPost("follow")]
        public async Task<IActionResult> FollowUser(FriendFollower ff)
        {
            var res = await service.FollowUser(ff);
            if (res is null)
            {
                return BadRequest("Something done gone went wrong when you tried stalking that person");
            }
            return Ok(res);
        }

        [HttpDelete("{userid:int}/unfollow/{targetuserid:int}")]
        public async Task<IActionResult> UnfollowUser(int userid, int targetuserid)
        {
            var res = await service.UnfollowUser(userid, targetuserid);
            if (!res)
            {
                return BadRequest("Unable to unfollow, sorry not sorry");
            }
            return Ok(res);
        }

        [HttpPut("{userid:int}/block/{targetuserid:int}")]
        public async Task<IActionResult> BlockUser(int userid, int targetuserid)
        {
            var res = await service.BlockUser(userid, targetuserid);
            if (res is null)
            {
                return BadRequest("Jimmy was here (°ʖ°)╭∩╮");
            }
            return Ok(res);
        }

        [HttpDelete("{userid:int}/unblock/{targetuserid:int}")]
        public async Task<IActionResult> UnblockUser(int userid, int targetuserid)
        {
            var res = await service.UnblockUser(userid, targetuserid);
            if (!res)
            {
                return BadRequest("Unable to unblock, sorry not sorry");
            }
            return Ok(res);
        }

        [HttpDelete("{userid:int}/unfriend/{targetuserid:int}")]
        public async Task<IActionResult> RemoveFriend(int userid, int targetuserid)
        {
            var res = await service.RemoveFriend(userid, targetuserid);
            if (!res)
            {
                return BadRequest("The pizza is aggressive");
            }
            return Ok(res);
        }
    }
}
