using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IChatRepository chatRepo;
        private readonly IMessageRepository repo;
        public MessageService(IMessageRepository repo, IChatRepository chatRepo) { this.repo = repo; this.chatRepo = chatRepo; }
        public async Task<Message> CreateMessage(Message message)
        {
            var chat = await chatRepo.GetChatById(message.ChatId);           
            if (!chat.Users.Any(u => u.UserId == message.UserId))
            {
                return null;
            }
            message.TimeStamp = DateTime.Now;
            chat.TimeCreated = message.TimeStamp;

            await chatRepo.UpdateChat(chat);

            return await repo.AddMessage(message);
        }

        public async Task<Message> DeleteMessage(int id)
        {
            return await repo.DeleteMessage(id);
        }

        public async Task<Message> UpdateMessage(Message message)
        {
            return await repo.UpdateMessage(message);
        }
    }
}
