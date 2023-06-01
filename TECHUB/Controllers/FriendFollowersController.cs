using Microsoft.AspNetCore.Mvc;
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
    }
}
