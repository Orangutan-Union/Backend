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

        [HttpDelete("{userId:int}/{groupId:int}")]
        public async Task<IActionResult> DeleteGroupUser(int userId, int groupId)
        {
            return Ok(await service.DeleteGroupUser(userId, groupId));
        }
    }
}
