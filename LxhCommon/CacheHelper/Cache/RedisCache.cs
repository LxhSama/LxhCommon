using LxhCommon.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace LxhCommon.Cache;

/// <summary>
/// Redis缓存.
/// </summary>
public class RedisCache : ICache
{
    public static AppWebApplicationBuilderExtensions.Options options => AppWebApplicationBuilderExtensions.options;
    /// <summary>
    /// 构造函数.
    /// </summary>
    public RedisCache()
    {
        CSRedis.CSRedisClient? csredis = new CSRedis.CSRedisClient(options.RedisCoon);
        RedisHelper.Initialization(csredis);
    }

    /// <summary>
    /// 用于在 key 存在时删除 key.
    /// </summary>
    /// <param name="key">键.</param>
    public long Del(params string[] key)
    {
        return RedisHelper.Del(key);
    }

    /// <summary>
    /// 用于在 key 存在时删除 key.
    /// </summary>
    /// <param name="key">键.</param>
    public Task<long> DelAsync(params string[] key)
    {
        return RedisHelper.DelAsync(key);
    }

    /// <summary>
    /// 用于在 key 模板存在时删除.
    /// </summary>
    /// <param name="pattern">key模板.</param>
    public async Task<long> DelByPatternAsync(string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return default;

        // pattern = Regex.Replace(pattern, @"\{.*\}", "*");
        string[]? keys = await RedisHelper.KeysAsync(pattern);
        if (keys?.Length > 0)
        {
            return await RedisHelper.DelAsync(keys);
        }

        return default;
    }

    /// <summary>
    /// 检查给定 key 是否存在.
    /// </summary>
    /// <param name="key">键.</param>
    public bool Exists(string key)
    {
        return RedisHelper.Exists(key);
    }

    /// <summary>
    /// 检查给定 key 是否存在.
    /// </summary>
    /// <param name="key">键.</param>
    public Task<bool> ExistsAsync(string key)
    {
        return RedisHelper.ExistsAsync(key);
    }

    /// <summary>
    /// 获取指定 key 的增量值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="incrBy">增量.</param>
    /// <returns></returns>
    public long Incrby(string key, long incrBy)
    {
        return RedisHelper.IncrBy(key, incrBy);
    }

    /// <summary>
    /// 获取指定 key 的增量值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="incrBy">增量.</param>
    /// <returns></returns>
    public Task<long> IncrbyAsync(string key, long incrBy)
    {
        return RedisHelper.IncrByAsync(key, incrBy);
    }

    /// <summary>
    /// 获取指定 key 的值.
    /// </summary>
    /// <param name="key">键.</param>
    public string Get(string key)
    {
        return RedisHelper.Get(key);
    }

    /// <summary>
    /// 获取指定 key 的值.
    /// </summary>
    /// <typeparam name="T">byte[] 或其他类型.</typeparam>
    /// <param name="key">键.</param>
    public T Get<T>(string key)
    {
        return RedisHelper.Get<T>(key);
    }

    /// <summary>
    /// 获取指定 key 的值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    public Task<string> GetAsync(string key)
    {
        return RedisHelper.GetAsync(key);
    }

    /// <summary>
    /// 获取指定 key 的值.
    /// </summary>
    /// <typeparam name="T">byte[] 或其他类型.</typeparam>
    /// <param name="key">键.</param>
    public Task<T> GetAsync<T>(string key)
    {
        return RedisHelper.GetAsync<T>(key);
    }

    /// <summary>
    /// 设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    public bool Set(string key, object value)
    {
        return RedisHelper.Set(key, value);
    }

    /// <summary>
    /// 设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <param name="expire">有效期.</param>
    public bool Set(string key, object value, TimeSpan expire)
    {
        return RedisHelper.Set(key, value, expire);
    }

    /// <summary>
    /// 设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    public Task<bool> SetAsync(string key, object value)
    {
        return RedisHelper.SetAsync(key, value);
    }

    /// <summary>
    /// 保存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <param name="expire">过期时间.</param>
    /// <returns></returns>
    public Task<bool> SetAsync(string key, object value, TimeSpan expire)
    {
        return RedisHelper.SetAsync(key, value, expire);
    }

    /// <summary>
    /// 只有在 key 不存在时设置 key 的值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <param name="expire">有效期.</param>
    public bool SetNx(string key, object value, TimeSpan expire)
    {
        if (RedisHelper.SetNx(key, value))
        {
            RedisHelper.Set(key, value, expire);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 只有在 key 不存在时设置 key 的值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    public bool SetNx(string key, object value)
    {
       return RedisHelper.SetNx(key, value);
    }

    /// <summary>
    /// 获取所有key.
    /// </summary>
    /// <returns></returns>
    public List<string> GetAllKeys()
    {
        return RedisHelper.Keys("*").ToList();
    }

    /// <summary>
    /// 获取缓存过期时间.
    /// </summary>
    /// <param name="key">键值.</param>
    /// <returns></returns>
    public DateTime GetCacheOutTime(string key)
    {
        long second = RedisHelper.PTtl(key);
        return DateTime.Now.AddMilliseconds(second);
    }
}