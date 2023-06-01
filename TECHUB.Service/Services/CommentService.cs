using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHUB.Repository.Interfaces;
using TECHUB.Repository.Models;
using TECHUB.Service.Interfaces;

namespace TECHUB.Service.Services
{
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository repo;
        public CommentService(ICommentRepository repo) { this.repo = repo; }

        public async Task<Comment> CreateComment(Comment comment)
        {
            Comment newComment = new Comment();
            newComment.UserId = comment.UserId;
            newComment.PostId = comment.PostId;
            newComment.Content = comment.Content;
            newComment.TimeStamp = DateTime.Now;
            return await repo.CreateComment(newComment);
        }

        public async Task<Comment> DeleteComment(int id)
        {
            return await repo.DeleteComment(id);
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await repo.GetCommentById(id);
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            return await repo.UpdateComment(comment);
        }
    }
}
