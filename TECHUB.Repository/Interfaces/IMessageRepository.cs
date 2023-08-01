using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> AddMessage(Message message);
        Task<Message> DeleteMessage(int id);
        Task<Message> UpdateMessage(Message message);
    }
}
