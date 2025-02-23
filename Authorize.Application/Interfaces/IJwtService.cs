using Authorize.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Authorize.Application.Interfaces
{
    public interface IJwtService<TokenObj, TokenResponse> 
        where TokenObj: class 
        where TokenResponse : class
    {
        string GenerateToken(TokenObj tokenObject);

        Result<TokenResponse> DecodeToken(string token);
    }
}
