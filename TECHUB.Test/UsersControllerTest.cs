using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using TECHUB.API.Controllers;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
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
            User user = new User();
            user = null;
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
    }
}
