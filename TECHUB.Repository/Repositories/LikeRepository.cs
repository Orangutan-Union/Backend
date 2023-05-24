using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHUB.Repository.Context;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext context;
        public LikeRepository(DatabaseContext context) { this.context = context; }

        public async Task<Like> AddLike(Like like)
        {
            context.Add(like);
            await context.SaveChangesAsync();

            return like;
        }

        public async Task<Like> DeleteLike(int id)
        {
            var like = await context.Likes.FindAsync(id);

            if(like != null)
            {
                context.Likes.Remove(like);
                await context.SaveChangesAsync();
            }

            return like;
        }

        public async Task<Like> GetLike(Like like)
        {
            return await context.Likes.FirstOrDefaultAsync(l => l.UserId == like.UserId && l.PostId == like.PostId 
            || l.UserId == like.UserId && l.CommentId == like.CommentId);
        }

        public async Task<Like> UpdateLike(Like like)
        {
            context.Entry(like).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return like;
        }
    }
}
