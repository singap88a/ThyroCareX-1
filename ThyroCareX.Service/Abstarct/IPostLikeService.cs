using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Service.Abstarct
{
    public interface IPostLikeService
    {
        Task<bool> AddLikeAsync(PostLike postLike);
    }
}
