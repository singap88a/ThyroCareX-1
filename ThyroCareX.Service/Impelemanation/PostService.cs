using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class PostService:IPostService
    {
        #region Prop
        private readonly IPostRepository _postRepository;
        #endregion
        #region Constructor
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
            
        }

        #endregion
        #region Handle Functions
        public async Task<string> AddPostAsync(Post post)
        {
            await _postRepository.AddAsync(post);
            return " Doctor Added Successfully";
            
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            return await _postRepository.GetByIdAsync(id);
        }


        #endregion
    }
}
