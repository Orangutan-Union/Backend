using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<User> AddUser(AddUserViewModel userRequest);
        Task<User> DeleteUser(int id);
        Task<LoginViewModel> Login(LoginViewModel loginRequest);
    }
}
