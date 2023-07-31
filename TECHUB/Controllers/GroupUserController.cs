using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private readonly IGroupUserService service;
        public GroupUserController(IGroupUserService service) { this.service = service; }

        //[HttpGet("GetGroupJoinRequests/{id:int}/{type:type}")]
        //public async Task<IActionResult> GetGroupJoinRequests(int id, int type)
        //{
        //    return Ok(await service.G)
        //}

        [HttpPost]
        public async Task<IActionResult> AddGroupUser(GroupUser groupUser)
        {
            if (groupUser == null)
            {
                return BadRequest("Group is null");
            }

            return Ok(await service.AddGroupUser(groupUser));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupUser(int id)
        {
            return Ok(await service.DeleteGroupUser(id));
        }
    }
}
