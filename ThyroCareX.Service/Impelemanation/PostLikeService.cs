using Azure;
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
    public class PostLikeService:IPostLikeService
    {
        #region Feildes
        private readonly IPostLikeRepository _postLikerepository;
        #endregion
        #region Cons
        public PostLikeService(IPostLikeRepository postLikerepository) 
        {
            _postLikerepository = postLikerepository;
        }

        public async Task<bool> AddLikeAsync(PostLike postLike)
        {

            var existingLike = await _postLikerepository.GetTableAsTracking()
    .FirstOrDefaultAsync(x => x.PostId == postLike.PostId
                           && x.DoctorId == postLike.DoctorId);


            if (existingLike != null)
            {
                existingLike.IsActive = !existingLike.IsActive;
                await _postLikerepository.UpdateAsync(existingLike);

                return existingLike.IsActive; 
            }

            
            postLike.IsActive = true;
            await _postLikerepository.AddAsync(postLike);

            return true;
        }

        #endregion
        #region Handle Functions
      

    }

    #endregion

}

