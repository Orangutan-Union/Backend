using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> GetChatById(int id);
        Task<Chat> GetChatByUsers(int userId, int otherUserId);
        Task<Chat> AddChat(Chat chat);
        Task<Chat> AddUserToChat(Chat chat);
        Task<Chat> UpdateChat(Chat chat);
        Task<Chat> LeaveChat(int chatId, int userId);
        
    }
}
