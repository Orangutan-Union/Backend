using Microsoft.AspNetCore.Http;
using TECHUB.Repository.Models;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByUsername(string username);
        Task<List<User>> GetUsersBySearch(string search);
        Task<User> AddUser(AddUserViewModel userRequest);
        Task<User> DeleteUser(int id);
        Task<AuthenticatedResponse> Login(LoginViewModel loginRequest);
        Task<User> UpdateUser(User userReq);
        Task<bool> ChangePassword(ChangePasswordViewModel viewModel);
        Task<User> UploadProfileImage(IFormFile file, int id);
    }
}
