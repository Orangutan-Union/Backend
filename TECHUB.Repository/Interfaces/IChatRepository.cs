using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChatById(int id);
        Task<List<Chat>> GetUserChats(int id);
        Task<Chat> AddChat(Chat chat);
        Task<Chat> UpdateChat(Chat chat);
        Task<Chat> LeaveChat(int chatId, int userId);
        Task<Chat> DeleteChat(int id);
        
    }
}
