using CSharpFunctionalExtensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Test.Application.Contracts.Teacher;
using Test.Application.Interfaces;

namespace Test.Infastructure.Implementations
{
    public class TokenService : ITokenService<TeacherToken>
    {
        public Result<TeacherToken> DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecuriryToken = tokenHandler.ReadJwtToken(token);

            var email = jwtSecuriryToken.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var role = jwtSecuriryToken.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (email is null ||
                role is null)
            {
                return Result.Failure<TeacherToken>("Invalid Token");
            }

            return Result.Success(new TeacherToken
            {
                Email = email,
                Role = role
            });
        }
    }
}
