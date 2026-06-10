using Microsoft.Extensions.Caching.Distributed;
using StarterKit.Api.BuildingBlocks.Domain.Interfaces;
using System.Text.Json;
namespace StarterKit.Api.BuildingBlocks.Caching.Redis;

public sealed class RedisCacheService(IDistributedCache cache):IRedisCacheService
{ 
    public async Task<T?> GetAsync<T>(string key,CancellationToken ct=default)
    {
        var json=await cache.GetStringAsync(key,ct); 
        return json is null ? default:JsonSerializer.Deserialize<T>(json); 
    }
    public Task SetAsync<T>(string key,T value,TimeSpan ttl,CancellationToken ct=default)
        =>cache.SetStringAsync(key,JsonSerializer.Serialize(value),
            new DistributedCacheEntryOptions{AbsoluteExpirationRelativeToNow=ttl},ct); 
}
