using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContext;

        public UserContextService(IHttpContextAccessor httpContext) 
        {
            _httpContext = httpContext;
        }
        public string UserId => _httpContext.HttpContext?
            .User
            .FindFirst(ClaimTypes.NameIdentifier)?
            .Value!;
    }
}
