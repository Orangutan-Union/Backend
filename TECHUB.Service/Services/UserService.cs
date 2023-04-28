using System.Security.Cryptography;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;

        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }

        public async Task<List<User>> GetUsers()
        {
            return await repo.GetUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await repo.GetUserById(id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await repo.GetUserByUsername(username);
        }

        public async Task<List<User>> GetUsersBySearch(string search)
        {
            var users = await repo.GetUsers();

            users = users.Where(x => x.DisplayName.ToLower().Contains(search.ToLower())).ToList();

            return users;
        }

        public async Task<User> AddUser(AddUserViewModel userRequest)
        {
            // Takes password string input and turns it into encrypted Hash and Salt.
            CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User();
            newUser.Username = userRequest.Username;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;
            newUser.Email = userRequest.Email;
            newUser.DisplayName = userRequest.DisplayName;
            //TODO - Jimmy: Set a default picture once Picture repo/service/controller has been made.


            return await repo.AddUser(newUser);
        }

        public async Task<User> DeleteUser(int id)
        {
            return await repo.DeleteUser(id);
        }

        public async Task<LoginViewModel> Login(LoginViewModel loginRequest)
        {
            var user = await repo.GetUserByUsername(loginRequest.Username);

            if (user is null)
            {
                return null;
            }

            // Check if password is correct
            if (!VerifyPasswordHash(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            loginRequest.UserId = user.UserId;
            loginRequest.Username = user.Username;
            loginRequest.Password = string.Empty;           

            return loginRequest;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
