using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IMessageRepository
    {
        public Task<Message> AddMessage(Message message);
        public Task<Message> DeleteMessage(int id);
        public Task<Message> UpdateMessage(Message message);
    }
}
