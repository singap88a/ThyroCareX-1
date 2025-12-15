using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ThyroCareX.Data.Healpers;
using ThyroCareX.Data.Models.Identity;
using ThyroCareX.Service.Abstarct;

namespace ThyroCareX.Service.Impelemanation
{
    public class AuthentcationService: IAuthentcationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion
        #region constructor
        public AuthentcationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;

        }
        #endregion
        #region Handle Methods
        public Task<string> GetJWTToken(User user)
        {
            var cliams = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber)

            };
            var jwtToken = new JwtSecurityToken(
                
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                cliams,
                expires:DateTime.Now.AddMinutes(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return Task.FromResult(accessToken);




        }
        #endregion
    }
}
