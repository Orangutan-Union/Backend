using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace TECHUB.Repository.Repositories
{
    public class ChatRepository : IChatRepository

    {
        private readonly DatabaseContext context;
        public ChatRepository(DatabaseContext context) { this.context = context; }
        public async Task<Chat> AddChat(Chat chat)
        {
            context.Chats.Add(chat);            
            await context.SaveChangesAsync();

            return chat;
        }

        public async Task<Chat> GetChatById(int id)
        {
            return await context.Chats.Include(c => c.Users).FirstOrDefaultAsync(c => c.ChatId == id);
        }

        public async Task<List<Chat>> GetUserChats(int id)
        {
            var user = await context.Users.Include(u => u.Chats).ThenInclude(c => c.Messages).FirstOrDefaultAsync(u => u.UserId == id);
            var chats = user.Chats.OrderByDescending(c => c.Messages.Max(m => m.TimeStamp)).ToList();

            return chats;
        }

        public async Task<Chat> LeaveChat(int chatId, int userId)
        {
            var chat = await context.Chats.Include(c => c.Users).FirstOrDefaultAsync(c => c.ChatId == chatId);
            if (chat.Users.Count == 1)
            {
                context.Chats.Remove(chat);
            }
            else if (chat.Users.Count > 1)
            {
               foreach(var user in chat.Users)
                {
                    if(user.UserId == userId)
                    {
                        chat.Users.Remove(user);
                        context.Entry(chat).State = EntityState.Modified;
                    }
                }
            }
            
            await context.SaveChangesAsync();
            return chat;
        }

        public async Task<Chat> UpdateChat(Chat chat)
        {
            context.Entry(chat).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return chat;
        }
    }
}
