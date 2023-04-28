using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChat(int id);
        Task<List<Chat>> GetChats();
        Task<Chat> AddChat(Chat chat);
        Task<Chat> UpdateChat(Chat chat);
        Task<Chat> LeaveChat(int chatId, int userId);
        Task<Chat> DeleteChat(int id);
        
    }
}
