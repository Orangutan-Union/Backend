using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHUB.Repository.Models;

namespace TECHUB.Repository.Interfaces
{
    public interface ILikeRepository
    {
        public Task<Like> GetLike(Like like);
        public Task<Like> AddLike(Like like);
        public Task<Like> UpdateLike(Like like);
        public Task<Like> DeleteLike(int id);
    }
}
