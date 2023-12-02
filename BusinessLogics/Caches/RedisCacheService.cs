namespace SRE.Program.WebAPI.BusinessLogics.Caches;

public class RedisCacheService : IRedisCacheService
{
    public T Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public bool Set(string key, object value)
    {
        throw new NotImplementedException();
    }
}
