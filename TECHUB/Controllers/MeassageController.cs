using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassageController: ControllerBase
    {
        private readonly IMessageService service;
        private readonly DatabaseContext context;
        public MeassageController(IMessageService service, DatabaseContext context) { this.service = service; this.context = context; }


        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            return Ok(await service.CreateMessage(message));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await service.DeleteMessage(id);

            if (message == null)
            {
                return NotFound($"Counld not find the message with Id = {id}");
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMeassage(Message message)
        {
            var updateMessage = await service.UpdateMessage(message);

            if (updateMessage == null)
            {
                return NotFound($"Counld not find the message");
            }

            return Ok(updateMessage);
        }
    }
}
