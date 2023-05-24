using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHUB.Repository.Models;

namespace TECHUB.Service.Interfaces
{
    public interface ILikeService
    {
        public Task<Like> AddLike(Like like);

        public Task<Like> DeleteLike(int id);
    }
}
