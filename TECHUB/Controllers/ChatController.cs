using Microsoft.AspNetCore.Mvc;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService service;
        public ChatController(IChatService service) { this.service = service; }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserChats(int id)
        {
            return Ok(await service.GetUserChats(id));
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> CreateChat(int id, Chat chat)
        {
            return Ok(await service.CreateChat(chat, id));
        }

        [HttpPut("AddUser/{userId:int}/{chatId:int}")]
        public async Task<IActionResult> AddUserToChat(int userId, int chatId)
        {
            return Ok(await service.AddUserToChat(userId, chatId));
        }

        [HttpPut("Leave/{userId:int}/{chatId:int}")]
        public async Task<IActionResult> LeaveChat(int userId, int chatId)
        {
            return Ok(await service.LeaveChat(userId, chatId));
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateChat(Chat chat)
        {
            return Ok(await service.UpdateChat(chat));
        }
    }
}
