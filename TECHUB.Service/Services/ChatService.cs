using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository repo;
        private readonly IUserRepository userRepo;
        public ChatService(IChatRepository repo, IUserRepository userRepo) { this.repo = repo; this.userRepo = userRepo; }
        public async Task<Chat> CreateChat(Chat chat)
        {
            chat.LastMessageSent = DateTime.Now;

            return await repo.AddChat(chat);
        }

        public async Task<Chat> AddUserToChat(int userId, int chatId)
        {
            var chat = await repo.GetChatById(chatId);
            chat.Users.Add(await userRepo.GetUserById(userId));r

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
