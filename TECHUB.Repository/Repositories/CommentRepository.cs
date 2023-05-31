using Microsoft.EntityFrameworkCore;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    internal class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContext context;
        public CommentRepository(DatabaseContext context) { this.context = context; }
        public async Task<Comment> CreateComment(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> DeleteComment(int id)
        {
            var comment = await context.Comments.FindAsync(id);

            if (comment != null)
            {
                context.Comments.Remove(comment);
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
