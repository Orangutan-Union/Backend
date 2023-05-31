using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository repo;
        public LikeService(ILikeRepository repo) { this.repo = repo; }

        public async Task<Like> AddLike(Like like)
        {            
            var oldLike = await repo.GetLike(like);

            if (oldLike == null)
            { 
                return await repo.AddLike(like);                
            }
            else if (oldLike.UserId == like.UserId && oldLike.PostId == like.PostId && oldLike.CommentId == like.CommentId)
            {
                
                oldLike.IsLiked = like.IsLiked;
                oldLike.IsDisliked = like.IsDisliked;

                return await repo.UpdateLike(oldLike);
            }
            else
            {
                return null;
            }
        }

        public async Task<Like> DeleteLike(int id)
        {
            return await repo.DeleteLike(id);
        }
    }
}
