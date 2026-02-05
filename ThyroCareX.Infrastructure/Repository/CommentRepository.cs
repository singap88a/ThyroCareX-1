using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Repository
{
    public class CommentRepository: GenericRepositoryAsync<Comment>,ICommentRepository
    {
        #region Feildes
        public readonly DbSet<Comment> _comments;

        #endregion
        #region Cons
        public CommentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
            _comments=dbContext.Set<Comment>();
        }

        #endregion
        #region Handle Function
        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _comments
                .AsNoTracking()
                .Include(x=>x.Doctor)
                .OrderByDescending(x=>x.CreatedAt)
                .ToListAsync();
             
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int id)
        {
            return await _comments.AsNoTracking().Include(x => x.Doctor)
                .Where(x => x.PostId == id)
                .OrderByDescending(x => x.CreatedAt).ToListAsync();
        }
        #endregion
    }
}
