using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FreelanceAppAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace FreelanceAppAPI.Services
{

    public class TokenBuilder : ITokenBuilder
    {
        private IConfiguration _configuration;
        
        public TokenBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public UserToken BuildToken(UserAccountModel userData, IList<string> roles, string userId)
        {
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.UniqueName,userData.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach(var role in roles){
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["connectionKey"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["APIKey:connectionKey"]));
            
            var singInCreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationDate = DateTime.UtcNow.AddMinutes(60);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience:null,
                claims: claims,
                expires: expirationDate,
                signingCredentials: singInCreds
            );

            var newToken = new UserToken(){
                Id =userId,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expirationDate
            };

            return newToken;
        }
    }
}