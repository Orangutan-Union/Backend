using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace TECHUB.Repository.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DatabaseContext context;
        public MessageRepository(DatabaseContext context) { this.context = context; }

        public async Task<Message> AddMessage(Message message)
        {
            context.Messages.Add(message);
            await context.SaveChangesAsync();

            return message;
        }

        public async Task<Message> DeleteMessage(int id)
        {
            var message = await context.Messages.FindAsync(id);
            context.Messages.Remove(message);
            await context.SaveChangesAsync();

            return message;
        }

        public async Task<Message> UpdateMessage(Message message)
        {
            context.Entry(message).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return message;
        }
    }
}
