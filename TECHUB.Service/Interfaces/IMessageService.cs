using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessage(Message message);
        Task<Message> UpdateMessage(Message message);
        Task<Message> DeleteMessage(int id);
    }
}
