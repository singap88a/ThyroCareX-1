using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        #endregion
        #region constructor
        public AuthentcationService(JwtSettings jwtSettings, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }
        #endregion
        #region Handle Methods
        public async Task<string> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = await GenerateJWTToken(user);

           return accessToken;

        }

        public async Task<List<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var cliams = new List<Claim>()
             {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(nameof(UserClaimModel.UserName), user.UserName ?? ""),
                new Claim(nameof(UserClaimModel.Email),user.Email?? ""),
                new Claim(nameof(UserClaimModel.PhoneNumber),user.PhoneNumber ?? "")
             };
            foreach (var role in roles)
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            cliams.AddRange(userClaims);
            return cliams;
        }
       

        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        #endregion
    }
}
