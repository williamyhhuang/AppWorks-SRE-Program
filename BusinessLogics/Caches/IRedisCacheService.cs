namespace SRE.Program.WebAPI.BusinessLogics.Caches;

public interface IRedisCacheService
{
    string Get(string key);

    bool Set(string key, string value);
}
