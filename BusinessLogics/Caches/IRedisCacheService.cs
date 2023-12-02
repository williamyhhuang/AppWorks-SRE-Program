namespace SRE.Program.WebAPI.BusinessLogics.Caches;

public interface IRedisCacheService
{
    T Get<T>(string key);

    bool Set(string key, object value);
}
