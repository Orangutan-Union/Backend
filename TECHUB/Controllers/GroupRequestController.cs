using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRequestController : ControllerBase
    {
        private readonly IGroupRequestService service;
        public GroupRequestController(IGroupRequestService service) { this.service = service; }

        [HttpGet("{userId:int}/{groupId:int}")]
        public async Task<IActionResult> GetGroupJoinRequest(int userId, int groupId)
        {
            return Ok(await service.GetGroupJoinRequest(userId, groupId));
        }

        [HttpGet("getgroupsjoinrequests/{groupId:int}/{type:int}")]
        public async Task<IActionResult> GetGroupsJoinRequests(int groupId, int type)
        {
            return Ok(await service.GetGroupsJoinRequests(groupId, type));
        }

        [HttpGet("getusersjoinrequests/{userId:int}/{type:int}")]
        public async Task<IActionResult> GetUsersJoinRequests(int userId, int type)
        {
            return Ok(await service.GetUsersJoinRequests(userId, type));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGroupRequest(GroupRequest groupRequest)
        {
            return Ok(await service.AddGroupRequest(groupRequest));
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptGroupRequest(GroupRequest groupRequest)
        { 
            return Ok(await service.AcceptGroupRequest(groupRequest));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupRequest(int userId, int groupId)
        {
            return Ok(await service.DeleteGroupRequest(userId, groupId));
        }
    }
}
