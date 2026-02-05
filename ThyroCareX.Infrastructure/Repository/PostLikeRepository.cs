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
    public class PostLikeRepository: GenericRepositoryAsync<PostLike>,IPostLikeRepository
    {
        private readonly DbSet<PostLike> _posts;
        public PostLikeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
            _posts=dbContext.Set<PostLike>();
        }
    }
}
