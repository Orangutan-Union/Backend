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

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupUser(int id)
        {
            return Ok(await service.DeleteGroupUser(id));
        }
    }
}
