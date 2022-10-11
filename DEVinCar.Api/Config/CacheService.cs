using Microsoft.Extensions.Caching.Memory;

namespace DEVinCar.Api.Config;

public class CacheService<TEntity>
{
    private readonly IMemoryCache _cache;
    private string _baseKey;
    private TimeSpan _expiracao = new TimeSpan(0,5,0);
    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Config(string basekey, TimeSpan expiracao)
    {
        _baseKey = basekey;
        _expiracao = expiracao;
    }

    public TEntity Set(string paramento, TEntity entity)
    {
        return _cache.Set<TEntity>(BaseCacheString(paramento), entity, _expiracao);
    }

    public bool TryGetValue(string paramento, out TEntity entity)
    {
        return _cache.TryGetValue<TEntity>(BaseCacheString(paramento), out entity);
    }

    public void Remove(string paramento)
    {
        _cache.Remove(BaseCacheString(paramento));
    }

    public string BaseCacheString(string parametro)
    {
        return $"{_baseKey}: {parametro}";
    }
}
