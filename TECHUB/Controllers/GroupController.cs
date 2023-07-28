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
                    Posts = g.Posts.Select(p => new
                    {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        GroupId = p.GroupId,
                        TimeStamp = p.TimeStamp,
                        Content = p.Content,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                        User = new
                        {
                            UserId = p.User.UserId,
                            DisplayName = p.User.DisplayName,
                            Picture = new
                            {
                                PictureId = p.User.Picture.PictureId,
                                ImageUrl = p.User.Picture.ImageUrl,
                            }
                        },
                        Group = new
                        {
                            GroupId = p.Group.GroupId,
                            GroupName = p.Group.GroupName,
                        },
                        Likes = p.Likes.Select(pl => new
                        {
                            LikeId = pl.LikeId,
                            UserId = pl.UserId,
                            PostId = pl.PostId,
                            IsLiked = pl.IsLiked,
                            IsDisliked = pl.IsDisliked,
                            User = new
                            {
                                UserId = pl.User.UserId,
                                DisplayName = pl.User.DisplayName,
                            },
                        }).ToList(),
                        Pictures = p.Pictures.Select(p => new
                        {
                            PictureId = p.PictureId,
                            PublicId = p.PublicId,
                            ImageName = p.ImageName,
                            ImageUrl = p.ImageUrl,
                        }).ToList(),
                        Comments = p.Comments.Select(pc => new
                        {
                            CommentId = pc.CommentId,
                            UserId = pc.UserId,
                            TimeStamp = pc.TimeStamp,
                            Content = pc.Content,
                            User = new
                            {
                                UserId = pc.User.UserId,
                                DisplayName = pc.User.DisplayName,
                                Picture = new
                                {
                                    PictureId = pc.User.Picture.PictureId,
                                    ImageUrl = pc.User.Picture.ImageUrl,
                                }
                            },
                            Likes = pc.Likes.Select(cl => new
                            {
                                LikeId = cl.LikeId,
                                UserId = cl.UserId,
                                CommentId = cl.CommentId,
                                IsLiked = cl.IsLiked,
                                IsDisliked = cl.IsDisliked,
                                User = new
                                {
                                    UserId = cl.User.UserId,
                                    DisplayName = cl.User.DisplayName,
                                }
                            }).ToList(),
                        }).ToList(),
                    }).OrderByDescending(p => p.TimeStamp).ToList(),
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

        [HttpPost]
        public async Task<IActionResult> AddGroup(Group group)
        {
            if (group == null)
            {
                return BadRequest("Group is null");
            }

            return Ok(await service.AddGroup(group));
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
