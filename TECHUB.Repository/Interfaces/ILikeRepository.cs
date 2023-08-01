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
        Task<Like> GetLike(Like like);
        Task<Like> AddLike(Like like);
        Task<Like> UpdateLike(Like like);
        Task<Like> DeleteLike(int id);
    }
}
