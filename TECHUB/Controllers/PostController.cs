using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;
using TECHUB.Service.ViewModels;

namespace TECHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;
        private readonly DatabaseContext context;

        public PostController(IPostService service, DatabaseContext context) { this.service = service; this.context = context; }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await service.GetPostById(id);

            if (post == null)
            {
                return NotFound($"Could not find the post with ID = {id}");
            }

            return Ok(post);
        }

        [HttpGet("user/{id:int}")]
        public async Task<IActionResult> GetUserPosts(int id)
        {
            var posts = await context.Posts
                .Where(u => u.UserId == id)
                .Select(p => new
                {
                    PostId = p.PostId,
                    UserId = p.UserId,
                    GroupId = p.GroupId,
                    TimeStamp = p.TimeStamp,
                    Content = p.Content,
                    FriendOnly = p.FriendOnly,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Comments = p.Comments.Select(c => new
                    {
                        CommentId = c.CommentId,
                        UserId = c.UserId,
                        TimeStamp = c.TimeStamp,
                        Content = c.Content,
                        Likes = c.Likes.Select(cl => new
                        {
                            IsLiked = cl.IsLiked,
                            IsDisliked = cl.IsDisliked
                        }).ToList()
                    }).ToList(),
                    User = new
                    {
                        UserId = p.User.UserId,
                        DisplayName = p.User.DisplayName,
                        Picture = new
                        {
                            PictureId = p.User.Picture.PictureId,
                            //ImageData = p.User.Picture.ImageData,
                        }
                    },
                    Likes = p.Likes.Select(pl => new
                    {
                        IsLiked = pl.IsLiked,
                        IsDisliked = pl.IsDisliked
                    }).ToList(),
                    Group = new
                    {
                        GroupId = p.Group.GroupId,
                        GroupName = p.Group.GroupName,
                        Picture = new
                        {
                            PictureId = p.Group.Picture.PictureId,
                            //ImageData = p.Group.Picture.ImageData,
                        }
                    },
                    Pictures = p.Pictures.Select(p => new
                    {
                        PictureId = p.PictureId,
                        //ImageData = p.ImageData,
                    }).ToList()
                })
                .OrderByDescending(p => p.TimeStamp)
                .ToListAsync();

            return Ok(posts);
        }

        [HttpGet("feed/{id:int}")]
        public async Task<IActionResult> GetUserFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Where(ff => ff.Type != 3 && ff.UserId == id || ff.Type != 3 && ff.OtherUserId == id)
                .Select(ff => new
                {
                    UserId = ff.UserId,
                    OtherUserId = ff.OtherUserId,
                    User = new
                    {
                        UserId = ff.User.UserId,
                        Posts = ff.User.Posts.Select(p => new
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Group = new
                            {
                                GroupId = p.Group.GroupId,
                                GroupName = p.Group.GroupName,
                                Picture = new
                                {
                                    PictureId = p.Group.Picture.PictureId,
                                    ImageUrl = p.Group.Picture.ImageUrl,
                                }
                            },
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
                            Likes = p.Likes.Select(pl => new
                            {
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new
                                {
                                    UserId = pl.User.UserId,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new
                                {
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new
                                    {
                                        UserId = cl.User.UserId,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    },
                    OtherUser = new
                    {
                        UserId = ff.OtherUser.UserId,
                        Posts = ff.OtherUser.Posts.Select(p => new
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Group = new
                            {
                                GroupId = p.Group.GroupId,
                                GroupName = p.Group.GroupName,
                                Picture = new
                                {
                                    PictureId = p.Group.Picture.PictureId,
                                    ImageUrl = p.Group.Picture.ImageUrl,
                                }
                            },
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
                            Likes = p.Likes.Select(pl => new
                            {
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new
                                {
                                    UserId = pl.User.UserId,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new
                                {
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new
                                    {
                                        UserId = cl.User.UserId,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    }
                }).ToListAsync();

            var list = new List<dynamic>();
            foreach (var item in friendFollowers)
            {
                if (item.UserId != id)
                {
                    foreach (var post in item.User.Posts)
                    {
                        list.Add(post);
                    }
                }
                else if (item.OtherUserId != id)
                {
                    foreach (var post in item.OtherUser.Posts)
                    {
                        list.Add(post);
                    }
                }
            }

            return Ok(list);
        }

        [HttpGet("followerfeed/{id:int}")]
        public async Task<IActionResult> GetUserFollowerFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Where(ff => ff.Type == 2 && ff.UserId == id || ff.Type == 2 && ff.OtherUserId == id)
                .Select(ff => new
                {
                    UserId = ff.UserId,
                    OtherUserId = ff.OtherUserId,
                    User = new
                    {
                        UserId = ff.User.UserId,
                        Posts = ff.User.Posts.Select(p => new
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Group = new
                            {
                                GroupId = p.Group.GroupId,
                                GroupName = p.Group.GroupName,
                                Picture = new
                                {
                                    PictureId = p.Group.Picture.PictureId,
                                    ImageUrl = p.Group.Picture.ImageUrl,
                                }
                            },
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
                            Likes = p.Likes.Select(pl => new
                            {
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new
                                {
                                    UserId = pl.User.UserId,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new
                                {
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new
                                    {
                                        UserId = cl.User.UserId,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    },
                    OtherUser = new
                    {
                        UserId = ff.OtherUser.UserId,
                        Posts = ff.OtherUser.Posts.Select(p => new
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Group = new
                            {
                                GroupId = p.Group.GroupId,
                                GroupName = p.Group.GroupName,
                                Picture = new
                                {
                                    PictureId = p.Group.Picture.PictureId,
                                    ImageUrl = p.Group.Picture.ImageUrl,
                                }
                            },
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
                            Likes = p.Likes.Select(pl => new
                            {
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new
                                {
                                    UserId = pl.User.UserId,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new
                                {
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new
                                    {
                                        UserId = cl.User.UserId,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    }
                }).ToListAsync();

            var list = new List<dynamic>();
            foreach (var item in friendFollowers)
            {
                if (item.UserId != id)
                {
                    foreach (var post in item.User.Posts)
                    {
                        list.Add(post);
                    }
                }
                else if (item.OtherUserId != id)
                {
                    foreach (var post in item.OtherUser.Posts)
                    {
                        list.Add(post);
                    }
                }
            }

            return Ok(list);
        }

        [HttpGet("friendfeed/{id:int}")]
        public async Task<IActionResult> GetUserFriendFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Where(ff => ff.Type == 1 && ff.UserId == id || ff.Type == 1 && ff.OtherUserId == id)
                .Select(ff => new
                {
                    UserId = ff.UserId,
                    OtherUserId = ff.OtherUserId,
                    User = new
                    {
                        UserId = ff.User.UserId,
                        Posts = ff.User.Posts.Select(p => new
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Group = new
                            {
                                GroupId = p.Group.GroupId,
                                GroupName = p.Group.GroupName,
                                Picture = new
                                {
                                    PictureId = p.Group.Picture.PictureId,
                                    ImageUrl = p.Group.Picture.ImageUrl,
                                }
                            },
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
                            Likes = p.Likes.Select(pl => new
                            {
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new
                                {
                                    UserId = pl.User.UserId,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new
                                {
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new
                                    {
                                        UserId = cl.User.UserId,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    },
                    OtherUser = new
                    {
                        UserId = ff.OtherUser.UserId,
                        Posts = ff.OtherUser.Posts.Select(p => new
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Group = new
                            {
                                GroupId = p.Group.GroupId,
                                GroupName = p.Group.GroupName,
                                Picture = new
                                {
                                    PictureId = p.Group.Picture.PictureId,
                                    ImageUrl = p.Group.Picture.ImageUrl,
                                }
                            },
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
                            Likes = p.Likes.Select(pl => new
                            {
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new
                                {
                                    UserId = pl.User.UserId,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new
                                {
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new
                                    {
                                        UserId = cl.User.UserId,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    }
                }).ToListAsync();

            var list = new List<dynamic>();
            foreach (var item in friendFollowers)
            {
                if (item.UserId != id)
                {
                    foreach (var post in item.User.Posts)
                    {
                        list.Add(post);
                    }
                }
                else if (item.OtherUserId != id)
                {
                    foreach (var post in item.OtherUser.Posts)
                    {
                        list.Add(post);
                    }
                }
            }

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostViewModel post)
        {
            return Ok(await service.AddPost(post));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await service.DeletePost(id);

            if (post == null)
            {
                return NotFound($"Could not find the post with ID = {id}");
            }

            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            var updatePost = await service.UpdatePost(post);

            if (updatePost == null)
            {
                return NotFound($"Could not find the post");
            }

            return Ok(updatePost);
        }
    }
}
