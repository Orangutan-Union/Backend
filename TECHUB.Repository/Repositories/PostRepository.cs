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
                .Include(p => p.Likes)
                .Include(p => p.User)
                .ThenInclude(u => u.Picture)
                .Include(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Likes)
                .Include(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.User)
                .ThenInclude(cu => cu.Picture)
                .Include(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
                .ThenInclude(cpc => cpc.Comment)
                .ThenInclude(cc => cc.Likes)
                .Include(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
                .ThenInclude(cpc => cpc.Comment)
                .ThenInclude(cc => cc.User)
                .ThenInclude(ccu => ccu.Picture)
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
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
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
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
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
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(ff => ff.User)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(ff => ff.OtherUser)
                .ThenInclude(u => u.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
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
            var user = await context.Users
                .Include(u => u.Posts)
                .ThenInclude(p => p.Likes)
                .Include(g => g.Posts)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.Picture)
                .Include(g => g.Posts)
                .ThenInclude(p => p.PicturePosts)
                .ThenInclude(pp => pp.Picture)
                .Include(g => g.Posts)
                .ThenInclude(p => p.Comments)
                .ThenInclude(pc => pc.Comment)
                .ThenInclude(c => c.Comments)
                .FirstOrDefaultAsync(u => u.UserId == id);

            return user.Posts;
        }

        public async Task<Post> UpdatePost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return post;
        }
    }
}
