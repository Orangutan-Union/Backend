﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;
        private readonly IPictureRepository pictureRepository;
        private readonly IConfiguration configuration;
        private readonly DatabaseContext context;

        public UserService(IUserRepository repo, IPictureRepository pictureRepository, IConfiguration configuration, DatabaseContext context)
        {
            this.repo = repo;
            this.pictureRepository = pictureRepository;
            this.configuration = configuration;
            this.context = context;
        }

        public async Task<List<User>> GetUsers()
        {
            return await repo.GetUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await repo.GetUserById(id);
            if (user is null)
            {
                return null;
            }

            return user;
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

            // Get Default pic from DB and set it as profile pic.
            var pic = await pictureRepository.GetPictureById(1);
            newUser.Picture = pic;


            return await repo.AddUser(newUser);
        }

        public async Task<User> DeleteUser(int id)
        {
            return await repo.DeleteUser(id);
        }

        public async Task<AuthenticatedResponse> Login(LoginViewModel loginRequest)
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

            string newAccessToken = CreateJwtToken();
            string newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
            await repo.UpdateUser(user);

            AuthenticatedResponse auth = new AuthenticatedResponse()
            {
                UserId = user.UserId,
                Username = loginRequest.Username,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };

            return auth;
        }

        public async Task<User> UpdateUser(User userReq)
        {
            var user = await repo.GetUserById(userReq.UserId);

            if (user is null)
            {
                return null;
            }

            user.DisplayName = userReq.DisplayName;
            user.Email = userReq.Email;

            return await repo.UpdateUser(user);
        }

        public async Task<User> UploadProfileImage(IFormFile file, int id)
        {
            var user = await repo.GetUserById(id);
            int? pictureId = 0;
            if (user is null)
            {
                return null;
            }
            else if (user.Picture is not null)
            {
                pictureId = user.Picture.PictureId;
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                if (memoryStream.Length < 2097152)
                {
                    var pic = new Picture()
                    {
                        ImageData = memoryStream.ToArray(),
                        ImageName = file.FileName,
                    };

                    // Set the uploaded picture and save it
                    user.Picture = pic;
                    await repo.UpdateUser(user);

                    // Delete the old picture from DB
                    if (pictureId != 1)
                    {
                        await pictureRepository.DeletePicture((int)pictureId);
                    }

                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel viewModel)
        {
            var user = await repo.GetUserById(viewModel.UserId);

            if (user is null)
            {
                return false;
            }

            //Verify that the old password is correct
            if (VerifyPasswordHash(viewModel.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                //Create new passwordHash and passwordSalt for the new password
                CreatePasswordHash(viewModel.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }
            else
            {
                return false;
            }

            await repo.UpdateUser(user);
            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
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

        public string CreateJwtToken()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signInCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            // Check if token exists in the Database already.
            var tokenInUser = context.Users.Any(u => u.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                // If token already exists then run the method again.
                return CreateRefreshToken();
            }
            return refreshToken;
        }
    }
}
