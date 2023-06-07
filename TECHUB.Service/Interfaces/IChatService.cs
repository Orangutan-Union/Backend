﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface IChatService
    {
        Task<Chat> GetUserChats(int id);
        Task<Chat> CreateChat(Chat chat);
        Task<Chat> UpdateChat(Chat chat);
        Task<Chat> LeaveChat(int id);
    }
}