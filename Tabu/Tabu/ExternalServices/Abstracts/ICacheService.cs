namespace Tabu.ExternalServices.Abstracts;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T data,int seconds = 300);
    Task RemoveAsync(string key);
}