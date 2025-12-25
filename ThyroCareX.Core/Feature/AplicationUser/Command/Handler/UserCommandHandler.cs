using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Core.Bases;
using ThyroCareX.Core.Feature.AplicationUser.Command.Models;
using ThyroCareX.Data.Models.Identity;

namespace ThyroCareX.Core.Feature.AplicationUser.Command.Handler
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        #region Faileds
        private readonly UserManager<User> _userManager;

        #endregion

        #region Constructor
        public UserCommandHandler(UserManager<User> userManager) 
        {
            _userManager = userManager;
        }
        #endregion

        #region Handle Function
        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //Check If user is exist or not
            var user = await _userManager.FindByEmailAsync(request.Email.Trim());
            //return the email not found
            if (user is null || !await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
                return BadRequest<string>("Email Or Password Is Wrong");
            // Change User Password
            var result= await _userManager.ChangePasswordAsync(user,request.CurrentPassword,request.NewPassword);
            if (!result.Succeeded) return BadRequest<string>("Change Password Falid");

            return Success("Change Pass Successful");
        }
        #endregion
    }
}
