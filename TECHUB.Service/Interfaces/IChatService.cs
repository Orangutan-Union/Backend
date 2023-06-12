using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChat(Chat chat, int id);
        Task<Chat> AddUserToChat(int userId, int chatId);
        Task<Chat> LeaveChat(int userId, int chatId);
        Task<Chat> UpdateChat(Chat chat);
    }
}
