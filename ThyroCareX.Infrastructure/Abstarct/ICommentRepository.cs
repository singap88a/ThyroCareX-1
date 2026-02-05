using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Abstarct
{
    public interface ICommentRepository:IGenericRepositoryAsync<Comment>
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<List<Comment>> GetCommentsByPostIdAsync(int id);
    }
}
