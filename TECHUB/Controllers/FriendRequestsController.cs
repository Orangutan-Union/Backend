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

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(FriendRequestViewModel viewmodel)
        {
            if (viewmodel.ReceiverId == 0 || viewmodel.SenderId == 0)
            {
                return BadRequest($"Invalid ID - SenderId: {viewmodel.SenderId} | ReceiverId: {viewmodel.ReceiverId}");
            }
            return Ok(await service.SendFriendRequest(viewmodel));
        }
    }
}
