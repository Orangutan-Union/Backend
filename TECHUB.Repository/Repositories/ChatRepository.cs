﻿using TECHUB.Repository.Context;
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

        public async Task<Chat> GetChatByUsers(int userId, int otherUserId)
        {
            var chats = await context.Chats
                .FirstOrDefaultAsync(c => c.IsPrivate == true & c.Users.Any(u => u.UserId == userId) & c.Users.Any(u => u.UserId == otherUserId));

            return chats;
        }

        public async Task<Chat> AddUserToChat(Chat chat)
        {
            var oldChat = await context.Chats.Include(c => c.Users).FirstOrDefaultAsync(c => c.ChatId == chat.ChatId);
            foreach (var user in chat.Users)
            {
                if (!oldChat.Users.Any(x => x.UserId == user.UserId))
                {
                    var finduser = await context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
                    oldChat.Users.Add(finduser);
                }
            }
            //context.Entry(oldChat).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return chat;
        }

        public async Task<Chat> LeaveChat(int userId, int chatId)
        {
            var chat = await context.Chats.Include(c => c.Users).FirstOrDefaultAsync(c => c.ChatId == chatId);
            var user = chat.Users.FirstOrDefault(x => x.UserId == userId);
            if (chat.IsPrivate == true)
            {
                return null;
            }
            else if (chat.Users.Count == 1)
            {
                context.Chats.Remove(chat);
            }
            else if (chat.Users.Count > 1)
            {
                if (user is null)
                {
                    return null;
                }

                chat.Users.Remove(user);

                context.Entry(chat).State = EntityState.Modified;
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
