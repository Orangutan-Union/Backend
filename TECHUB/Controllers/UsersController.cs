﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TECHUB.Repository.Context;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await service.GetUsers());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await service.GetUserById(id);

            if (user is null)
            {
                return NotFound($"Could not find user with ID = {id}");
            }

            return Ok(user);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetUsersBySearch(string search)
        {
            var users = await service.GetUsersBySearch(search);
            return Ok(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AddUserViewModel request)
        {
            var user = await service.GetUserByUsername(request.Username);

            if (user is not null)
            {
                return BadRequest("An account with the that username already exists.");
            }

            user = await service.AddUser(request);
            return Ok(user);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await service.DeleteUser(id);

            if (user is null)
            {
                return BadRequest($"Unable to find user with ID = {id}");
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            var user = await service.Login(request);

            if (user is null)
            {
                return BadRequest("Bad login");
            }

            return Ok(user);
        }

        [HttpPut("update/{id:int}")]
        public async Task<IActionResult> UpdateUser(User userReq)
        {
            var user = await service.UpdateUser(userReq);

            if (user is null)
            {
                return NotFound($"Unable to find user with ID = {userReq.UserId}");
            }

            return Ok(user);
        }

        [HttpPut("{id:int}/uploadimage")]
        public async Task<IActionResult> UploadProfileImage(int id)
        {
            var file = Request.Form.Files[0];
            var user = await service.UploadProfileImage(file, id);

            if (user is null)
            {
                return BadRequest($"Could not find user with ID = {id}");
            }

            return Ok();
        }

        [HttpPut("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            var success = await service.ChangePassword(viewModel);

            if (!success)
            {
                return BadRequest("Change Password failed.");
            }

            return Ok(success);
        }
    }
}
