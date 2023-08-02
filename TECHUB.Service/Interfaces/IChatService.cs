using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetChatByUsers(int userId, int otherUserId);
        Task<Chat> CreateChat(Chat chat, int id);
        Task<Chat> CreatePrivateChat(int senderId, int receiverId);
        Task<Chat> AddUserToChat(Chat chat);
        Task<Chat> LeaveChat(int userId, int chatId);
        Task<Chat> UpdateChat(Chat chat);
    }
}
