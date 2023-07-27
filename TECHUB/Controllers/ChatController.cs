using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService service;
        private readonly DatabaseContext context;
        public ChatController(IChatService service, DatabaseContext context) { this.service = service; this.context = context; }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetChatMessages(int id)
        {
            var chat = await context.Chats
                .Select(c => new
                {
                    ChatId = c.ChatId,
                    ChatName = c.ChatName,
                    IsPrivate = c.IsPrivate,
                    Messages = c.Messages.Select(m => new
                    {
                        MessageId = m.MessageId,
                        ChatId = m.ChatId,
                        UserId = m.UserId,
                        TimeStamp = m.TimeStamp,
                        Content = m.Content,
                        User = new
                        {
                            UserId = m.User.UserId,
                            DisplayName = m.User.DisplayName,
                            Picture = new
                            {
                                PictureId = m.User.Picture.PictureId,
                                ImageUrl = m.User.Picture.ImageUrl,
                            }
                        }
                    }).ToList(),
                    Users = c.Users.Select(u => new
                    {
                        UserId = u.UserId,
                        DisplayName = u.DisplayName,
                        Picture = new
                        {
                            PictureId = u.Picture.PictureId,
                            ImageUrl = u.Picture.ImageUrl,
                        }
                    }).ToList(),
                }).FirstOrDefaultAsync(c => c.ChatId == id);

            return Ok(chat);
        }
        [HttpGet("User/{id:int}")]
        public async Task<IActionResult> GetUserChats(int id)
        {
            var user = await context.Users
                .Select(u => new
                {
                    UserId = u.UserId,
                    DisplayName = u.DisplayName,
                    Chats = u.Chats.Select(c => new
                    {
                        ChatId = c.ChatId,
                        ChatName = c.ChatName,
                        LastMessageSent = c.LastMessageSent,
                        IsPrivate = c.IsPrivate,
                    }).OrderByDescending(c => c.LastMessageSent)
                    .ToList(),
                }).FirstOrDefaultAsync(u => u.UserId == id);

            return Ok(user.Chats);
        }

        [HttpPost("{userId:int}")]
        public async Task<IActionResult> CreateChat(int userId, Chat chat)
        {
            return Ok(await service.CreateChat(chat, userId));
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserToChat(Chat chat)
        {
            chat.LastMessageSent = DateTime.Now;
            var temp = await service.AddUserToChat(chat);
            return Ok(temp);
        }

        [HttpPut("Leave/{userId:int}/{chatId:int}")]
        public async Task<IActionResult> LeaveChat(int userId, int chatId)
        {
            return Ok(await service.LeaveChat(userId, chatId));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateChat(Chat chat)
        {
            chat.LastMessageSent = DateTime.Now;

            return Ok(await service.UpdateChat(chat));
        }
    }
}
