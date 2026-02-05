using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Service.Abstarct
{
    public interface ICommentService
    {
        Task<string> AddCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentsAsync();
        Task< List<Comment>>GetCommentsByPostIdAsync(int id);
    }
}
