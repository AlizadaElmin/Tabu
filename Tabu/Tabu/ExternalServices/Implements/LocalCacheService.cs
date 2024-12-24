using System.Data;
using Microsoft.Extensions.Caching.Memory;
using Tabu.ExternalServices.Abstracts;

namespace Tabu.ExternalServices.Implements;

public class LocalCacheService(IMemoryCache _cache):ICacheService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        T? value = default(T);
        await Task.Run(() =>
        {
            _cache.TryGetValue<T>(key, out value);
        });
        return value;
    }
    public async Task SetAsync<T>(string key, T data, int seconds = 300)
    {
     await Task.Run(() =>
     {
         _cache.Set<T>(key, data, DateTime.Now.AddSeconds(seconds));
     });
    }

    public async Task RemoveAsync(string key)
    {
        await Task.Run(() =>
        {
            _cache.Remove(key);
        });
    }
}