using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;

        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await context.Users.Include(u => u.Picture).ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await context.Users.Include(x => x.Picture).Include(x => x.SentFriendRequests).Include(x => x.ReceivedFriendRequests).FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> AddUser(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user is not null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }

            return user;
        }
    }
}
