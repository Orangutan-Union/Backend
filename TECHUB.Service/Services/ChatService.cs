﻿using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository repo;
        private readonly IUserRepository userRepo;
        public ChatService(IChatRepository repo, IUserRepository userRepo) { this.repo = repo; this.userRepo = userRepo; }
        public async Task<Chat> CreateChat(Chat chat, int id)
        {
            var user = await userRepo.GetUserById(id);

            chat.LastMessageSent = DateTime.Now;
            chat.IsPrivate = false;
            chat.Users.Add(user);

            return await repo.AddChat(chat);
        }


        public async Task<Chat> CreatePrivateChat(int senderId, int receiverId)
        {
            Chat chat = new Chat();
            User sender = await userRepo.GetUserById(senderId);
            User receiver = await userRepo.GetUserById(receiverId);

            chat.ChatName = sender.DisplayName + " & " + receiver.DisplayName;
            chat.IsPrivate = true;
            chat.Users.Add(sender);
            chat.Users.Add(receiver);

            return await repo.AddChat(chat);
        }

        public async Task<Chat> AddUserToChat(Chat chat)
        {
            return await repo.AddUserToChat(chat);
        }

        public async Task<Chat> LeaveChat(int userId, int chatId)
        {
            return await repo.LeaveChat(userId, chatId);
        }

        public async Task<Chat> UpdateChat(Chat chat)
        {
            return await repo.UpdateChat(chat);
        }

        public async Task<Chat> GetChatByUsers(int userId, int otherUserId)
        {
            return await repo.GetChatByUsers(userId, otherUserId);
        }
    }
}
