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
            chat.IsPrivate = false;

            return await repo.AddChat(chat);
        }


        public async Task<Chat> CreatePrivateChat(int senderId, int receiverId)
        {
            Chat chat = new Chat();
            User sender = await userRepo.GetUserById(senderId);
            User receiver = await userRepo.GetUserById(receiverId);

            chat.ChatName = sender.DisplayName + " & " + receiver.DisplayName;
            chat.IsPrivate = true;
            chat.Users.Add(sender);
            chat.Users.Add(receiver);

            return await repo.AddChat(chat);
        }

        public async Task<Chat> AddUserToChat(int userId, int chatId)
        {
            var chat = await repo.GetChatById(chatId);
            if (chat.Users.Any(c => c.UserId == userId))
            {
                return null;
            }
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
