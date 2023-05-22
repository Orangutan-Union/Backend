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
            return await context.Posts
                .Include(p => p.Pictures)
                .Include(p => p.Likes)
                .Include(p => p.User)
                .ThenInclude(u => u.Picture)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Likes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.User)
                .ThenInclude(cu => cu.Picture)
                .FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task<List<Post>> GetUserFeed(int id)
        {
            var friendFollowers = await context.FriendFollowers
                .Include(ff => ff.User)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                //.Include(ff => ff.User)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Pictures)
                //.Include(ff => ff.User)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Comments)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                //.Include(ff => ff.OtherUser)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.PicturePosts)
                //.ThenInclude(pp => pp.Picture)
                //.Include(ff => ff.OtherUser)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Comments)
                //.ThenInclude(pc => pc.Comment)
                //.ThenInclude(c => c.Comments)
                .Where(ff => ff.Type != 3 && ff.UserId == id || ff.Type != 3 && ff.OtherUserId == id).ToListAsync();

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
                .Include(ff => ff.User)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.PicturePosts)
                //.ThenInclude(pp => pp.Picture)
                //.Include(ff => ff.User)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Comments)
                //.ThenInclude(pc => pc.Comment)
                //.ThenInclude(c => c.Comments)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.PicturePosts)
                //.ThenInclude(pp => pp.Picture)
                //.Include(ff => ff.OtherUser)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Comments)
                //.ThenInclude(pc => pc.Comment)
                //.ThenInclude(c => c.Comments)
                .Where(ff => ff.Type == 2 && ff.UserId == id || ff.Type == 2 && ff.OtherUserId == id).ToListAsync();

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
                .Include(ff => ff.User)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.PicturePosts)
                //.ThenInclude(pp => pp.Picture)
                //.Include(ff => ff.User)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Comments)
                //.ThenInclude(pc => pc.Comment)
                //.ThenInclude(c => c.Comments)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.PicturePosts)
                //.ThenInclude(pp => pp.Picture)
                //.Include(ff => ff.OtherUser)
                //.ThenInclude(u => u.Posts)
                //.ThenInclude(p => p.Comments)
                //.ThenInclude(pc => pc.Comment)
                //.ThenInclude(c => c.Comments)
                .Where(ff => ff.Type == 1 && ff.UserId == id || ff.Type == 1 && ff.OtherUserId == id).ToListAsync();

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

                    }).ToList()

                })
                .ToListAsync();

            return posts;
        }

        //public async Task<List<Post>> GetUserPosts(int id)
        //{
        //    var posts = await context.Posts
        //        .Include(p => p.User).ThenInclude(u => u.Picture)
        //        .Include(p => p.Likes).ThenInclude(l => l.User)
        //        .Include(p => p.Pictures)
        //        .Include(p => p.Comments).ThenInclude(c => c.User)
        //        .Include(p => p.Comments).ThenInclude(c => c.Likes)
        //        .Where(u => u.UserId == id).ToListAsync();

        //    return posts;
        //}

        //public async Task<List<Post>> GetUserPosts(int id)
        //{
        //    var posts = await context.Posts
        //        .Where(p => p.UserId == id)
        //        .Select(p => new Post
        //        {
        //            PostId = p.PostId,
        //            UserId = p.UserId,
        //            GroupId = p.GroupId,
        //            TimeStamp = p.TimeStamp,
        //            Content = p.Content,
        //            FriendOnly = p.FriendOnly,
        //            Latitude = p.Latitude,
        //            Longitude = p.Longitude,
        //            Comments = p.Comments.Select(c => new Comment
        //            {
        //                CommentId = c.CommentId,
        //                UserId = c.UserId,
        //                TimeStamp = c.TimeStamp,
        //                Content = c.Content,
        //                Likes = c.Likes.Select(l => new Like
        //                {
        //                    LikeId = l.LikeId,
        //                    UserId = l.UserId,
        //                    IsLiked = l.IsLiked,
        //                    IsDisliked = l.IsDisliked
        //                }).ToList(),
        //                User = new User
        //                {
        //                    UserId = c.User.UserId,
        //                    ProfilePictureId = c.User.ProfilePictureId,
        //                    Username = c.User.Username,
        //                    Email = c.User.Email,
        //                    DisplayName = c.User.DisplayName
        //                }
        //            }).ToList(),
        //            Pictures = p.Pictures.Select(pic => new Picture
        //            {
        //                PictureId = pic.PictureId,
        //                ImageName = pic.ImageName,
        //                ImageData = pic.ImageData
        //            }).ToList(),
        //            User = new User
        //            {
        //                UserId = p.User.UserId,
        //                ProfilePictureId = p.User.ProfilePictureId,
        //                Username = p.User.Username,
        //                Email = p.User.Email,
        //                DisplayName = p.User.DisplayName,
        //                Picture = new Picture
        //                {
        //                    PictureId = p.User.Picture.PictureId,
        //                    ImageName = p.User.Picture.ImageName,
        //                    ImageData = p.User.Picture.ImageData
        //                }
        //            }
        //        })
        //        .ToListAsync();

        //    return posts;
        //}

        public async Task<Post> UpdatePost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return post;
        }
    }
}
