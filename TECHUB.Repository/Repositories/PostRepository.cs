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

        public async Task<Post> UpdatePost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return post;
        }
    }
}
