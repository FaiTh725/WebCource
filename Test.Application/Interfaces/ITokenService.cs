using CSharpFunctionalExtensions;

namespace Test.Application.Interfaces
{
    public interface ITokenService<DecodeObj>
    {
        Result<DecodeObj> DecodeToken(string token);
    }
}
