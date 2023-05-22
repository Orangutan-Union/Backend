using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using TECHUB.API.Controllers;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;
using Xunit;

namespace TECHUB.Test
{
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> service;
        private readonly UsersController controller;

        public UsersControllerTest()
        {
            service = new Mock<IUserService>();
            controller = new UsersController(service.Object);
        }

        [Fact]
        public async void GetUsers_Returns_OkObjectResult()
        {
            // Arrange
            service.Setup(x => x.GetUsers())
                .ReturnsAsync(new List<User>() { new User(), new User() });

            // Act
            var result = await controller.GetUsers();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetUsers_Returns_ListOfUsers()
        {
            // Arrange
            service.Setup(x => x.GetUsers())
                .ReturnsAsync(new List<User>() { new User(), new User() });

            // Act
            var result = await controller.GetUsers();
            var okResult = result as OkObjectResult;
            var actual = okResult?.Value;
            // Assert
            Assert.IsType<List<User>>(actual);
        }

        [Fact]
        public async void GetUserById_Returns_OkObjectResult()
        {
            // Arrange
            service.Setup(x => x.GetUserById(1))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.GetUserById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetUserById_Returns_Type_User()
        {
            // Arrange
            service.Setup(x => x.GetUserById(1))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.GetUserById(1);
            var okResult = result as OkObjectResult;
            var actual = okResult?.Value;

            // Assert
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async void GetUserById_Returns_NotFound_If_UserIsNull()
        {
            // Arrange
            User user = null;
            service.Setup(x => x.GetUserById(1))
                .ReturnsAsync(user);

            // Act
            var result = await controller.GetUserById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void GetUsersBySearch_Returns_OkObjectResult()
        {
            // Arrange
            service.Setup(x => x.GetUsersBySearch("gew"))
                .ReturnsAsync(new List<User>() { new User(), new User() });

            // Act
            var result = await controller.GetUsersBySearch("gew");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetUsersBySearch_Returns_ListOfUsers()
        {
            // Arrange
            service.Setup(x => x.GetUsersBySearch("gew"))
                .ReturnsAsync(new List<User>() { new User(), new User() });

            // Act
            var result = await controller.GetUsersBySearch("gew");
            var okResult = result as OkObjectResult;
            var actual = okResult?.Value;

            // Assert
            Assert.IsType<List<User>>(actual);
        }

        [Fact]
        public async void Register_Returns_BadRequest_If_UserAlreadyExists()
        {
            // Arrange
            AddUserViewModel newUser = new AddUserViewModel()
            {
                Username = "gew",
                Password = "1234",
                DisplayName = "Big Guy",
                Email = "bigguy@gmail.com"
            };
            service.Setup(x => x.GetUserByUsername("gew"))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.Register(newUser);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void Register_Returns_OkObjectResult_If_UserDoesntExists()
        {
            // Arrange
            User nullUser = null;
            AddUserViewModel newUser = new AddUserViewModel()
            {
                Username = "gew",
                Password = "1234",
                DisplayName = "Big Guy",
                Email = "bigguy@gmail.com"
            };

            service.Setup(x => x.GetUserByUsername("gew"))
                .ReturnsAsync(nullUser);

            // Act
            var result = await controller.Register(newUser);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Register_Returns_Type_User()
        {
            // Arrange
            AddUserViewModel newUser = new AddUserViewModel()
            {
                Username = "gew",
                Password = "1234",
                DisplayName = "Big Guy",
                Email = "bigguy@gmail.com"
            };

            service.Setup(x => x.AddUser(newUser))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.Register(newUser);
            var okResult = result as OkObjectResult;
            var actual = okResult?.Value;

            // Assert
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async void DeleteUser_Returns_BadRequest_If_UserIsNull()
        {
            // Arrange
            User nullUser = null;
            service.Setup(x => x.DeleteUser(1))
                .ReturnsAsync(nullUser);

            // Act
            var result = await controller.DeleteUser(1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteUser_Returns_NotFound_If_Successful()
        {
            // Arrange
            service.Setup(x => x.DeleteUser(1))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.DeleteUser(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void Login_Returns_UnAuthorized_If_BadLogin()
        {
            // Arrange
            AuthenticatedResponse response = null;
            LoginViewModel login = new LoginViewModel()
            {
                Username = "gew",
                Password = "1234"
            };

            service.Setup(x => x.Login(login))
                .ReturnsAsync(response);

            // Act
            var result = await controller.Login(login);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }

        [Fact]
        public async void Login_Returns_OkObjectResult_If_GoodLogin()
        {
            // Arrange
            LoginViewModel login = new LoginViewModel()
            {
                Username = "gew",
                Password = "1234"
            };

            service.Setup(x => x.Login(login))
                .ReturnsAsync(new AuthenticatedResponse());

            // Act
            var result = await controller.Login(login);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Login_Returns_Type_AuthenticatedResponse()
        {
            // Arrange
            LoginViewModel login = new LoginViewModel()
            {
                Username = "gew",
                Password = "1234"
            };

            service.Setup(x => x.Login(login))
                .ReturnsAsync(new AuthenticatedResponse());

            // Act
            var result = await controller.Login(login);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value;

            // Assert
            Assert.IsType<AuthenticatedResponse>(actual);
        }

        [Fact]
        public async void UpdateUser_Returns_OkObjectResult()
        {
            // Arrange
            var updatedUser = new User()
            {
                DisplayName = "monke"
            };

            service.Setup(x => x.UpdateUser(updatedUser))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.UpdateUser(updatedUser);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UpdateUser_Returns_Type_User()
        {
            // Arrange
            var updatedUser = new User()
            {
                DisplayName = "monke"
            };

            service.Setup(x => x.UpdateUser(updatedUser))
                .ReturnsAsync(new User());

            // Act
            var result = await controller.UpdateUser(updatedUser);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value;

            // Assert
            Assert.IsType<User>(actual);
        }

        [Fact]
        public async void UpdateUser_Returns_NotFound_If_UserNotFound()
        {
            // Arrange
            var updatedUser = new User()
            {
                DisplayName = "monke"
            };
            User nullUser = null;

            service.Setup(x => x.UpdateUser(updatedUser))
                .ReturnsAsync(nullUser);

            // Act
            var result = await controller.UpdateUser(updatedUser);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void ChangePassword_Returns_BadRequest_If_Unsuccessful()
        {
            // Arrange
            ChangePasswordViewModel pass = new ChangePasswordViewModel()
            {
                UserId = 1,
                OldPassword = "1234",
                NewPassword = "12345"
            };

            service.Setup(x => x.ChangePassword(pass))
                .ReturnsAsync(false);

            // Act
            var result = await controller.ChangePassword(pass);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void ChangePassword_Returns_OkObjectResult_If_Successful()
        {
            // Arrange
            ChangePasswordViewModel pass = new ChangePasswordViewModel()
            {
                UserId = 1,
                OldPassword = "1234",
                NewPassword = "12345"
            };

            service.Setup(x => x.ChangePassword(pass))
                .ReturnsAsync(true);

            // Act
            var result = await controller.ChangePassword(pass);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void ChangePassword_Returns_Type_Boolean()
        {
            // Arrange
            ChangePasswordViewModel pass = new ChangePasswordViewModel()
            {
                UserId = 1,
                OldPassword = "1234",
                NewPassword = "12345"
            };

            service.Setup(x => x.ChangePassword(pass))
                .ReturnsAsync(true);

            // Act
            var result = await controller.ChangePassword(pass);
            var okResult = result as OkObjectResult;
            var actual = okResult.Value;

            // Assert
            Assert.IsType<bool>(actual);
        }

        [Fact]
        public async void RefreshToken_Returns_BadRequest_If_AuthResponseIsNull()
        {
            // Arrange
            AuthenticatedResponse nullResponse = null;


            // Act
            var result = await controller.RefreshToken(nullResponse);


            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void RefreshToken_Returns_BadRequest_If_UserIsNull()
        {
            // Arrange
            AuthenticatedResponse authResponse = new AuthenticatedResponse()
            {
                UserId = 1,
                AccessToken = "cool access token",
                RefreshToken = "not quite as cool refresh token"
            };
            User nullUser = null;

            service.Setup(x => x.GetUserById(1))
                .ReturnsAsync(nullUser);

            // Act
            var result = await controller.RefreshToken(authResponse);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void RefreshToken_Returns_BadRequest_If_RefreshTokenMismatch()
        {
            // Arrange
            AuthenticatedResponse authResponse = new AuthenticatedResponse()
            {
                UserId = 1,
                AccessToken = "cool access token",
                RefreshToken = "not quite as cool refresh token"
            };
            User user = new User()
            {
                RefreshToken = "beep boop",
                RefreshTokenExpiryTime = System.DateTime.Now.AddDays(1)
            };

            service.Setup(x => x.GetUserById(1))
                .ReturnsAsync(user);

            // Act
            var result = await controller.RefreshToken(authResponse);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void RefreshToken_Returns_BadRequest_If_RefreshTokenExpired()
        {
            // Arrange
            AuthenticatedResponse authResponse = new AuthenticatedResponse()
            {
                UserId = 1,
                AccessToken = "cool access token",
                RefreshToken = "not quite as cool refresh token"
            };
            User user = new User()
            {
                RefreshToken = "not quite as cool refresh token",
                RefreshTokenExpiryTime = System.DateTime.Now.AddDays(-1)
            };

            service.Setup(x => x.GetUserById(1))
                .ReturnsAsync(user);

            // Act
            var result = await controller.RefreshToken(authResponse);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}
