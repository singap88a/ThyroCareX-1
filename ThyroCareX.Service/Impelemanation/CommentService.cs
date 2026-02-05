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
    public class CommentService:ICommentService
    {
        #region Fiedls
        private readonly ICommentRepository _commentRepository;
        #endregion
        #region Cons
        public CommentService(ICommentRepository commentRepository)
        {
            
            _commentRepository = commentRepository;
        }

        #endregion
        #region Handle Functions
        public async Task<string> AddCommentAsync(Comment comment)
        {
            await _commentRepository.AddAsync(comment);
            return "Comment Added Successfully";
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllCommentsAsync();

        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int id)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(id);
            

        }



        #endregion
    }
}
