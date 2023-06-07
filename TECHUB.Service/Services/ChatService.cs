using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository repo;
        public ChatService(IChatRepository repo) { this.repo = repo; }
        public async Task<Chat> CreateChat(Chat chat)
        {
            Chat newChat = new Chat();
            newChat = chat;
            newChat.TimeCreated = DateTime.Now;

            return await repo.AddChat(newChat);
        }

        public async Task<List<Chat>> GetUserChats(int id)
        {
            return await repo.GetUserChats(id);
        }

        public async Task<Chat> AddUserToChat(int userId, int chatId)
        {
            var chat = await repo.GetChatById(chatId);
            chat.Users.Add(await userRepo.GetUserById(userId));

            return await repo.UpdateChat(chat);
        }

        public async Task<Chat> LeaveChat(int userId, int chatId)
        {
            return await repo.LeaveChat(userId, chatId);
        }

        public async Task<Chat> UpdateChat(Chat chat)
        {
            return await repo.UpdateChat(chat);
        }
    }
}
