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
    public class PostRepository: GenericRepositoryAsync<Post>,IPostRepository
    {
        #region Prop
        public readonly DbSet<Post> _post;
        #endregion
        #region Constructor
        public PostRepository(ApplicationDbContext dbcontext) : base(dbcontext)
        {
            _post = dbcontext.Set<Post>();
            
        }

        #endregion
        #region HandleFunction
        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _post.AsNoTracking()
                .Include(x=>x.Doctor)
                .Include(x=>x.Comments)
                .Include(x=>x.PostLikes)
                .OrderByDescending(x=>x.CreatedAt)
                .ToListAsync();
        }

        #endregion
    }
}
