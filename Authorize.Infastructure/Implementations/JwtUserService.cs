using Application.Shared.Exceptions;
using Authorize.Application.Contacts.Token;
using Authorize.Application.Contacts.User;
using Authorize.Application.Interfaces;
using Authorize.Infastructure.Configurations;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Authorize.Infastructure.Implementations
{
    public class JwtUserService : 
        IJwtService<GenerateUserToken, TokenResponse>
    {
        private readonly IConfiguration configuration;

        public JwtUserService(
            IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Result<TokenResponse> DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            var email = jwtSecurityToken.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = jwtSecurityToken.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if(email is null ||
                role is null)
            {
                return Result.Failure<TokenResponse>("Token doesnt have required claims");
            }

            return Result.Success(new TokenResponse
            {
                Email = email,
                Role = role,
            });
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    
        public string GenerateToken(GenerateUserToken tokenObject)
        {
            var jwtConf = configuration
                .GetSection("JwtSetting")
                .Get<JwtConf>() ??
                throw new AppConfigurationException("Jwt setting");

            var signinngCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConf.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, tokenObject.Email),
                new Claim(ClaimTypes.Role, tokenObject.RoleName)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signinngCredentials,
                audience: jwtConf.Audience,
                issuer: jwtConf.Issuer,
                expires: DateTime.UtcNow.AddMinutes(15));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
