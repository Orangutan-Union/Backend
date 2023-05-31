using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment);
        Task<Comment> DeleteComment(int id);
    }
}
