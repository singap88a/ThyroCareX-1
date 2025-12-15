using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models.Identity;

namespace ThyroCareX.Service.Abstarct
{
    public interface IAuthentcationService
    {
        Task<string> GetJWTToken(User user);
    }
}
