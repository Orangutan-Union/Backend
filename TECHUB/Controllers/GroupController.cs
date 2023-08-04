using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService service;
        private readonly DatabaseContext context;
        public GroupController(IGroupService service, DatabaseContext context) { this.service = service; this.context = context; }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await context.Groups
                .Select(g => new
                {
                    GroupId = g.GroupId,
                    PictureId = g.PictureId,
                    GroupName = g.GroupName,
                    TimeCreated = g.TimeCreated,
                    Picture = new
                    {
                        PictureId = g.Picture.PictureId,
                        ImageUrl = g.Picture.ImageUrl,
                    },                    
                    GroupUser = g.GroupUsers.Select(gu => new
                    {
                        GroupId = gu.GroupId,
                        UserId = gu.UserId,
                        Type = gu.Type,
                        User = new
                        {
                            DisplayName = gu.User.DisplayName,
                            Picture = new
                            {
                                PictureId = gu.User.Picture.PictureId,
                                ImageUrl = gu.User.Picture.ImageUrl,
                            }
                        }
                    }).ToList(),
                }).FirstOrDefaultAsync(g => g.GroupId == id);

            return Ok(group);
        }

        [HttpGet("groupusers/{id:int}")]
        public async Task<IActionResult> GetGroupUsers(int id)
        {
            var group = await context.Groups
                .Select(g => new
                {
                    GroupId = g.GroupId,
                    PictureId = g.PictureId,
                    GroupName = g.GroupName,
                    TimeCreated = g.TimeCreated,
                    Picture = new
                    {
                        PictureId = g.Picture.PictureId,
                        ImageUrl = g.Picture.ImageUrl,
                    },
                    GroupUser = g.GroupUsers.Select(gu => new
                    {
                        GroupId = gu.GroupId,
                        UserId = gu.UserId,
                        Type = gu.Type,
                        User = new
                        {
                            DisplayName = gu.User.DisplayName,
                            Picture = new
                            {
                                PictureId = gu.User.Picture.PictureId,
                                ImageUrl = gu.User.Picture.ImageUrl,
                            }
                        }
                    }).ToList(),
                }).FirstOrDefaultAsync(g => g.GroupId == id);

            return Ok(group);
        }

        [HttpGet("usersgroups/{id:int}")]
        public async Task<IActionResult> GetUsersGroups(int id)
        {
            var user = await context.Users
                .Select(u => new
                {
                    UserId = u.UserId,
                    GroupUser = u.GroupUsers.Select(gu => new
                    {
                        GroupId = gu.GroupId,
                        UserId = gu.UserId,
                        Type = gu.Type,
                        Group = new
                        {
                            GroupId = gu.Group.GroupId,
                            PictureId = gu.Group.PictureId,
                            GroupName = gu.Group.GroupName,
                            TimeCreated = gu.Group.TimeCreated,
                            Picture = new
                            {
                                PictureId = gu.Group.Picture.PictureId,
                                ImageUrl = gu.Group.Picture.ImageUrl,
                            },
                        },
                    }).ToList(),
                }).FirstOrDefaultAsync(u => u.UserId == id);

            return Ok(user);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> AddGroup(int id, Group group)
        {
            if (group == null)
            {
                return BadRequest("Group is null");
            }

            return Ok(await service.AddGroup(group, id));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await service.DeleteGroup(id);

            if (group == null)
            {
                return NotFound($"Could not find the group with ID = {id}");
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(Group group)
        {
            return Ok(await service.UpdateGroup(group));
        }
    }
}
