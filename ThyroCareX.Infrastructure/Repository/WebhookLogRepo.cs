using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Context;
using ThyroCareX.Infrastructure.InfrastructureBases;

namespace ThyroCareX.Infrastructure.Repository
{
    public class WebhookLogRepo:GenericRepositoryAsync<WebhookLog>,IWebhookLogRepo
    {
        public readonly DbSet<WebhookLog> _log;
        public WebhookLogRepo(ApplicationDbContext dbContext):base(dbContext)
        {
            _log=dbContext.Set<WebhookLog>();


        }
    }
}
