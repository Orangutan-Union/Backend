using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext context;
        public PostRepository(DatabaseContext context) { this.context = context; }
        public async Task<Post> AddPost(Post post)
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();

            return post;
        }

        public async Task<Post> DeletePost(int id)
        {
            var post = await context.Posts.FindAsync(id);

            if (post != null)
            {
                context.Posts.Remove(post);
                await context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await context.Posts
                .Include(p => p.Pictures)
                .Include(p => p.Likes)
                .Include(p => p.User).ThenInclude(u => u.Picture)
                .Include(p => p.Comments).ThenInclude(c => c.Likes).ThenInclude(cl => cl.User)
                .Include(p => p.Comments).ThenInclude(c => c.User).ThenInclude(cu => cu.Picture)
                .Select(p => new Post
                {
                    PostId = p.PostId,
                    UserId = p.UserId,
                    GroupId = p.GroupId,
                    TimeStamp = p.TimeStamp,
                    Content = p.Content,
                    FriendOnly = p.FriendOnly,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    User = new User
                    {
                        UserId = p.User.UserId,
                        Username = p.User.Username,
                        DisplayName = p.User.DisplayName,
                        Picture = new Picture
                        {
                            PictureId = p.User.Picture.PictureId,
                            ImageName = p.User.Picture.ImageName,
                            ImageData = p.User.Picture.ImageData,
                        }
                    },
                    Likes = p.Likes.Select(pl => new Like
                    {
                        LikeId = pl.LikeId,
                        UserId = pl.UserId,
                        PostId = pl.PostId,
                        CommentId = pl.CommentId,
                        IsLiked = pl.IsLiked,
                        IsDisliked = pl.IsDisliked,
                        User = new User
                        {
                            UserId = pl.User.UserId,
                            Username = pl.User.Username,
                            DisplayName = pl.User.DisplayName,
                        }
                    }).ToList(),
                    Comments = p.Comments.Select(pc => new Comment
                    {
                        CommentId = pc.CommentId,
                        UserId = pc.UserId,
                        TimeStamp = pc.TimeStamp,
                        Content = pc.Content,
                        User = new User
                        {
                            UserId = pc.User.UserId,
                            Username = pc.User.Username,
                            DisplayName = pc.User.DisplayName,
                            Picture = new Picture
                            {
                                PictureId = pc.User.Picture.PictureId,
                                ImageName = pc.User.Picture.ImageName,
                                ImageData = pc.User.Picture.ImageData,
                            }
                        },
                        Likes = pc.Likes.Select(cl => new Like
                        {
                            LikeId = cl.LikeId,
                            UserId = cl.UserId,
                            PostId = cl.PostId,
                            CommentId = cl.CommentId,
                            IsLiked = cl.IsLiked,
                            IsDisliked = cl.IsDisliked,
                            User = new User
                            {
                                UserId = cl.User.UserId,
                                Username = cl.User.Username,
                                DisplayName = cl.User.DisplayName,
                            }
                        }).ToList(),
                    }).ToList(),
                }).FirstOrDefaultAsync();

            return post;
        }

        public async Task<List<Post>> GetUserFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Include(ff => ff.User).ThenInclude(u => u.Picture)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Likes)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Pictures)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Comments)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Pictures)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Comments)
                .Where(ff => ff.Type != 3 && ff.UserId == id || ff.Type != 3 && ff.OtherUserId == id)
                .Select(ff => new FriendFollower
                {
                    UserId = ff.UserId,
                    OtherUserId = ff.OtherUserId,
                    User = new User
                    {
                        UserId = ff.User.UserId,
                        Posts = ff.User.Posts.Select(p => new Post
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Likes = p.Likes.Select(pl => new Like
                            {
                                LikeId = pl.LikeId,
                                UserId = pl.UserId,
                                PostId = pl.PostId,
                                CommentId = pl.CommentId,
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new User
                                {
                                    UserId = pl.User.UserId,
                                    Username = pl.User.Username,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new Comment
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new Like
                                {
                                    LikeId = cl.LikeId,
                                    UserId = cl.UserId,
                                    PostId = cl.PostId,
                                    CommentId = cl.CommentId,
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new User
                                    {
                                        UserId = cl.User.UserId,
                                        Username = cl.User.Username,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    },
                    OtherUser = new User
                    {
                        UserId = ff.OtherUser.UserId,
                        Posts = ff.OtherUser.Posts.Select(p => new Post
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Likes = p.Likes.Select(pl => new Like
                            {
                                LikeId = pl.LikeId,
                                UserId = pl.UserId,
                                PostId = pl.PostId,
                                CommentId = pl.CommentId,
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new User
                                {
                                    UserId = pl.User.UserId,
                                    Username = pl.User.Username,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new Comment
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new Like
                                {
                                    LikeId = cl.LikeId,
                                    UserId = cl.UserId,
                                    PostId = cl.PostId,
                                    CommentId = cl.CommentId,
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new User
                                    {
                                        UserId = cl.User.UserId,
                                        Username = cl.User.Username,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    }
                }).ToListAsync();

            var list = new List<Post>();
            foreach (var item in friendFollowers)
            {
                if (item.User.UserId != id)
                {
                    foreach (var post in item.User.Posts)
                    {
                        list.Add(post);
                    }
                }
            }

            return list;
        }

        public async Task<List<Post>> GetUserFollowerFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Include(ff => ff.User).ThenInclude(u => u.Picture)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Likes)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Pictures)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Comments)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Pictures)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Comments)
                .Where(ff => ff.Type == 2 && ff.UserId == id || ff.Type == 2 && ff.OtherUserId == id)
                .Select(ff => new FriendFollower
                {
                    UserId = ff.UserId,
                    OtherUserId = ff.OtherUserId,
                    User = new User
                    {
                        UserId = ff.User.UserId,
                        Posts = ff.User.Posts.Select(p => new Post
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Likes = p.Likes.Select(pl => new Like
                            {
                                LikeId = pl.LikeId,
                                UserId = pl.UserId,
                                PostId = pl.PostId,
                                CommentId = pl.CommentId,
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new User
                                {
                                    UserId = pl.User.UserId,
                                    Username = pl.User.Username,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new Comment
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new Like
                                {
                                    LikeId = cl.LikeId,
                                    UserId = cl.UserId,
                                    PostId = cl.PostId,
                                    CommentId = cl.CommentId,
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new User
                                    {
                                        UserId = cl.User.UserId,
                                        Username = cl.User.Username,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    },
                    OtherUser = new User
                    {
                        UserId = ff.OtherUser.UserId,
                        Posts = ff.OtherUser.Posts.Select(p => new Post
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Likes = p.Likes.Select(pl => new Like
                            {
                                LikeId = pl.LikeId,
                                UserId = pl.UserId,
                                PostId = pl.PostId,
                                CommentId = pl.CommentId,
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new User
                                {
                                    UserId = pl.User.UserId,
                                    Username = pl.User.Username,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new Comment
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new Like
                                {
                                    LikeId = cl.LikeId,
                                    UserId = cl.UserId,
                                    PostId = cl.PostId,
                                    CommentId = cl.CommentId,
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new User
                                    {
                                        UserId = cl.User.UserId,
                                        Username = cl.User.Username,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    }
                }).ToListAsync();

            var list = new List<Post>();
            foreach (var item in friendFollowers)
            {
                if (item.User.UserId != id)
                {
                    foreach (var post in item.User.Posts)
                    {
                        list.Add(post);
                    }
                }
            }

            return list;
        }

        public async Task<List<Post>> GetUserFriendFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Include(ff => ff.User).ThenInclude(u => u.Picture)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Likes)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Pictures)
                .Include(ff => ff.User).ThenInclude(u => u.Posts).ThenInclude(p => p.Comments)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Pictures)
                .Include(ff => ff.OtherUser).ThenInclude(u => u.Posts).ThenInclude(p => p.Comments)
                .Where(ff => ff.Type == 1 && ff.UserId == id || ff.Type == 1 && ff.OtherUserId == id)
                .Select(ff => new FriendFollower
                {
                    UserId = ff.UserId,
                    OtherUserId = ff.OtherUserId,
                    User = new User
                    {
                        UserId = ff.User.UserId,
                        Posts = ff.User.Posts.Select(p => new Post
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Likes = p.Likes.Select(pl => new Like
                            {
                                LikeId = pl.LikeId,
                                UserId = pl.UserId,
                                PostId = pl.PostId,
                                CommentId = pl.CommentId,
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new User
                                {
                                    UserId = pl.User.UserId,
                                    Username = pl.User.Username,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new Comment
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new Like
                                {
                                    LikeId = cl.LikeId,
                                    UserId = cl.UserId,
                                    PostId = cl.PostId,
                                    CommentId = cl.CommentId,
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new User
                                    {
                                        UserId = cl.User.UserId,
                                        Username = cl.User.Username,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    },
                    OtherUser = new User
                    {
                        UserId = ff.OtherUser.UserId,
                        Posts = ff.OtherUser.Posts.Select(p => new Post
                        {
                            PostId = p.PostId,
                            UserId = p.UserId,
                            GroupId = p.GroupId,
                            TimeStamp = p.TimeStamp,
                            Content = p.Content,
                            FriendOnly = p.FriendOnly,
                            Latitude = p.Latitude,
                            Longitude = p.Longitude,
                            Likes = p.Likes.Select(pl => new Like
                            {
                                LikeId = pl.LikeId,
                                UserId = pl.UserId,
                                PostId = pl.PostId,
                                CommentId = pl.CommentId,
                                IsLiked = pl.IsLiked,
                                IsDisliked = pl.IsDisliked,
                                User = new User
                                {
                                    UserId = pl.User.UserId,
                                    Username = pl.User.Username,
                                    DisplayName = pl.User.DisplayName,
                                }
                            }).ToList(),
                            Comments = p.Comments.Select(c => new Comment
                            {
                                CommentId = c.CommentId,
                                UserId = c.UserId,
                                TimeStamp = c.TimeStamp,
                                Content = c.Content,
                                Likes = c.Likes.Select(cl => new Like
                                {
                                    LikeId = cl.LikeId,
                                    UserId = cl.UserId,
                                    PostId = cl.PostId,
                                    CommentId = cl.CommentId,
                                    IsLiked = cl.IsLiked,
                                    IsDisliked = cl.IsDisliked,
                                    User = new User
                                    {
                                        UserId = cl.User.UserId,
                                        Username = cl.User.Username,
                                        DisplayName = cl.User.DisplayName,
                                    }
                                }).ToList(),
                            }).ToList(),
                        }).ToList()
                    }
                }).ToListAsync();

            var list = new List<Post>();
            foreach (var item in friendFollowers)
            {
                if (item.User.UserId != id)
                {
                    foreach (var post in item.User.Posts)
                    {
                        list.Add(post);
                    }
                }
            }

            return list;
        }

        public async Task<List<Post>> GetUserPosts(int id)
        {
            var posts = await context.Posts
                .Include(p => p.User).ThenInclude(u => u.Picture)
                .Include(p => p.Likes)
                .Include(p => p.Pictures)
                .Include(p => p.Comments)
                .Where(u => u.UserId == id)
                .Select(p => new Post
                {
                    PostId = p.PostId,
                    UserId = p.UserId,
                    GroupId = p.GroupId,
                    Content = p.Content,
                    FriendOnly = p.FriendOnly,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    Comments = p.Comments.Select(c => new Comment
                    {
                        CommentId = c.CommentId,
                        UserId = c.UserId,
                        TimeStamp = c.TimeStamp,
                        Content = c.Content,
                        Likes = c.Likes.Select(cl => new Like
                        {
                            LikeId = cl.LikeId,
                            UserId = cl.UserId,
                            PostId = cl.PostId,
                            CommentId = cl.CommentId,
                            IsLiked = cl.IsLiked,
                            IsDisliked = cl.IsDisliked
                        }).ToList()
                    }).ToList(),
                    User = new User
                    {
                        UserId = p.User.UserId,
                        Username = p.User.Username,
                        DisplayName = p.User.DisplayName
                    },
                    Likes = p.Likes.Select(pl => new Like
                    {
                        LikeId = pl.LikeId,
                        UserId = pl.UserId,
                        PostId = pl.PostId,
                        CommentId = pl.CommentId,
                        IsLiked = pl.IsLiked,
                        IsDisliked = pl.IsDisliked
                    }).ToList(),
                    Group = new Group
                    {
                        GroupId = p.Group.GroupId,
                        GroupName = p.Group.GroupName,
                    },
                    Pictures = p.Pictures.Select(p => new Picture
                    {
                        PictureId = p.PictureId,
                        ImageName = p.ImageName,
                        ImageData = p.ImageData,
                    }).ToList()

                })
                .ToListAsync();

            return posts;
        }

        public async Task<Post> UpdatePost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return post;
        }
    }
}
