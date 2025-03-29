using CSharpFunctionalExtensions;

namespace Test.Application.Interfaces
{
    public interface ICachService
    {
        Task<Result<T>> GetData<T>(string key);

        Task SetData<T>(string key, T value, int secondExpired);

        Task RemoveData(string key);
    }
}
