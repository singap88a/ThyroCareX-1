using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Models;
using ThyroCareX.Infrastructure.Abstarct;
using ThyroCareX.Infrastructure.Repository;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class CommentService : ICommentService
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

        public async Task<string> DeleteCommentAsync(Comment comment)
        {
            var trans =_commentRepository.BeginTransaction();
            try
            {
                await _commentRepository.DeleteAsync(comment);
                await trans.CommitAsync();
                return "Success";
            }
            catch
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllCommentsAsync();

        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            var comment=await _commentRepository.GetByIdAsync(id);
            return comment;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int id)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(id);
            

        }




        #endregion
    }
}
