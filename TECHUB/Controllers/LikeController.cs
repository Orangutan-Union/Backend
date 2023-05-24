using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService service;
        public LikeController(ILikeService service) { this.service = service; }

        [HttpPost]
        public async Task<IActionResult> AddLike(Like like)
        {
            return Ok(await service.AddLike(like));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            var like = await service.DeleteLike(id);

            if (like == null)
            {
                return NotFound($"Could not find the Like with ID = {id}");
            }

            return NoContent();
        }
    }
}
