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
            Like oldLike = await repo.GetLike(like);

            if (oldLike == null)
            {
                Like newLike = new Like();
                newLike.LikeId = like.LikeId;
                newLike.UserId = like.UserId;
                newLike.PostId = like.PostId;
                newLike.CommentId = like.CommentId;
                newLike.IsLiked = like.IsLiked;
                newLike.IsDisliked = like.IsDisliked;

                return await repo.AddLike(newLike);
            }
            else
            {
                Like updateLike = new Like();
                updateLike.LikeId = oldLike.LikeId;
                updateLike.UserId = oldLike.UserId;
                updateLike.PostId = oldLike.PostId;
                updateLike.CommentId = oldLike.CommentId;
                updateLike.IsLiked = like.IsLiked;
                updateLike.IsDisliked = like.IsDisliked;

                return await repo.UpdateLike(updateLike);
            }
        }

        public async Task<Like> DeleteLike(int id)
        {
            return await repo.DeleteLike(id);
        }
    }
}
