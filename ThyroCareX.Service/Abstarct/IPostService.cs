using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;

namespace ThyroCareX.Service.Abstarct
{
    public interface IPostService
    {
        Task<string> AddPostAsync(Post post);
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
    }
}
