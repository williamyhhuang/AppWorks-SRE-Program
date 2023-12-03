using StackExchange.Redis;

namespace SRE.Program.WebAPI.BusinessLogics.Caches;

public class RedisCacheService : IRedisCacheService
{
    private ILogger<RedisCacheService> _logger;
    private ConnectionMultiplexer _redis;

    public RedisCacheService(
        ILogger<RedisCacheService> logger,
        IConfiguration configuration)
    {
        this._logger = logger;
        var endPoint = configuration.GetSection("RedisSettings:EndPoint").Value;
        this._redis = ConnectionMultiplexer.Connect($"{endPoint}");
    }

    public string Get(string key)
    {
        var db = this._redis.GetDatabase();
        var result = db.StringGet(key);

        return result.ToString();
    }

    public bool Set(string key, string value)
    {
        this._logger.LogInformation($"key:{key} is settled");

        var db = this._redis.GetDatabase();
        var result = db.StringSet(key, value, expiry: TimeSpan.FromHours(1), when: When.NotExists);

        return result;
    }
}
