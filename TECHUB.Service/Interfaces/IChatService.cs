using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IChatService
    {
        Task<List<Chat>> GetUserChats(int id);
        Task<Chat> CreateChat(Chat chat, int id);
        Task<Chat> AddUserToChat(Chat chat, int id);
        Task<Chat> LeaveChat(int userId, int chatId);
        Task<Chat> UpdateChat(Chat chat);
    }
}
