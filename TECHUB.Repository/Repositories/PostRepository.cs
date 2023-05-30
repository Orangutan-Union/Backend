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

        public async Task<Post> UpdatePost(Post post)
        {
            context.Entry(post).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return post;
        }
    }
}
