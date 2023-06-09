using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository repo;
        public MessageService(IMessageRepository repo) { this.repo = repo; }
        public async Task<Message> CreateMessage(Message message)
        {
            message.TimeStamp = DateTime.Now;
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
