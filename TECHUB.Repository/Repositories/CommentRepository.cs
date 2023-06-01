using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext context;
        public CommentRepository(DatabaseContext context) { this.context = context; }
        public async Task<Comment> GetCommentById(int id)
        {
            return await context.Comment
                .Include(c => c.User).ThenInclude(u => u.Picture)
                .FirstOrDefaultAsync(c => c.CommentId == id);
        }
        public async Task<Comment> CreateComment(Comment comment)
        {
            context.Comment.Add(comment);
            await context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> DeleteComment(int id)
        {
            var comment = await context.Comment.FindAsync(id);

            if (comment != null)
            {
                context.Comment.Remove(comment);
                await context.SaveChangesAsync();
            }

            return comment;
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            context.Entry(comment).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return comment;
        }
    }
}
