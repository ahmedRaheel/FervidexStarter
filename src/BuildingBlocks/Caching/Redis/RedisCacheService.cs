using Microsoft.Extensions.Caching.Distributed; using System.Text.Json;
namespace StarterKit.Api.BuildingBlocks.Caching.Redis;
public interface IRedisCacheService { Task<T?> GetAsync<T>(string key,CancellationToken ct=default); Task SetAsync<T>(string key,T value,TimeSpan ttl,CancellationToken ct=default); }
public sealed class RedisCacheService(IDistributedCache cache):IRedisCacheService
{ public async Task<T?> GetAsync<T>(string key,CancellationToken ct=default){ var json=await cache.GetStringAsync(key,ct); return json is null?default:JsonSerializer.Deserialize<T>(json); } public Task SetAsync<T>(string key,T value,TimeSpan ttl,CancellationToken ct=default)=>cache.SetStringAsync(key,JsonSerializer.Serialize(value),new DistributedCacheEntryOptions{AbsoluteExpirationRelativeToNow=ttl},ct); }
