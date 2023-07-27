using Microsoft.AspNetCore.Mvc;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendRequestsController : ControllerBase
    {
        private readonly IFriendRequestService service;

        public FriendRequestsController(IFriendRequestService service)
        {
            this.service = service;
        }

        [HttpGet("received/{id:int}")]
        public async Task<IActionResult> GetReceivedRequests(int id)
        {
            return Ok(await service.GetReceivedRequests(id));
        }

        [HttpGet("sent/{id:int}")]
        public async Task<IActionResult> GetSentRequests(int id)
        {
            return Ok(await service.GetSentRequests(id));
        }

        [HttpGet("all/{id:int}")]
        public async Task<IActionResult> GetAllRequests(int id)
        {
            return Ok(await service.GetAllRequests(id));
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(FriendRequestViewModel viewmodel)
        {
            if (viewmodel.ReceiverId == 0 || viewmodel.SenderId == 0)
            {
                return BadRequest($"Invalid ID - SenderId: {viewmodel.SenderId} | ReceiverId: {viewmodel.ReceiverId}");
            }

            var tt = await service.SendFriendRequest(viewmodel);

            if (tt is null)
            {
                return BadRequest("User is already a friend.");
            }
            return Ok(tt);
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptFriendRequest(FriendRequestViewModel friendRequest)
        {
            var res = await service.AcceptFriendRequest(friendRequest);

            if (res is null)
            {
                return BadRequest("An error occurred while trying to accept the friend request.");
            }
            return Ok(res);
        }

        [HttpPost("decline")]
        public async Task<IActionResult> DeclineFriendRequest(FriendRequestViewModel friendRequest)
        {
            var res = await service.DeclineFriendRequest(friendRequest);

            if (!res)
            {
                return BadRequest("An error occurred when trying to decline the friend request.");
            }
            return Ok(res);
        }
    }
}
