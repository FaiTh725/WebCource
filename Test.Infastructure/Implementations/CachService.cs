using CSharpFunctionalExtensions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;
using Test.Application.Interfaces;

namespace Test.Infastructure.Implementations
{
    public class CachService : ICachService
    {
        private readonly IDistributedCache cache;

        private JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public CachService(
            IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<Result<T>> GetData<T>(string key)
        {
            var jsonData = await cache.GetStringAsync(key);

            if(jsonData is null)
            {
                return Result.Failure<T>("Data doesnt set");
            }

            var data = JsonSerializer.Deserialize<T>(
                jsonData, serializerOptions);
        
            if(data is null)
            {
                return Result.Failure<T>("data serialized with error");
            }

            return data;
        }

        public async Task RemoveData(string key)
        {
            await cache.RemoveAsync(key);
        }

        public async Task SetData<T>(string key, T value, int secondExpired)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(secondExpired)
            };

            var jsonData = JsonSerializer.Serialize(value, serializerOptions);

            await cache.SetStringAsync(key, jsonData, cacheOptions);
        }
    }
}
