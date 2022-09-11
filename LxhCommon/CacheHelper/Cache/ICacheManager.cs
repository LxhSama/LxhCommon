namespace LxhCommon.Cache;

/// <summary>
/// 缓存管理抽象.
/// </summary>
public interface ICacheManager
{
    /// <summary>
    /// 获取所有缓存关键字.
    /// </summary>
    /// <returns></returns>
    List<string> GetAllCacheKeys();

    /// <summary>
    /// 删除指定关键字缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    bool Del(string key);

    /// <summary>
    /// 删除指定关键字缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    Task<bool> DelAsync(string key);

    /// <summary>
    /// 删除指定关键字数组缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    Task<bool> DelAsync(string[] key);

    /// <summary>
    /// 删除某特征关键字缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    Task<bool> DelByPatternAsync(string key);

    /// <summary>
    /// 设置缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <returns></returns>
    bool Set(string key, object value);

    /// <summary>
    /// 设置缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <param name="timeSpan">过期时间.</param>
    /// <returns></returns>
    bool Set(string key, object value, TimeSpan timeSpan);

    /// <summary>
    /// 设置缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <returns></returns>
    Task<bool> SetAsync(string key, object value);

    /// <summary>
    /// 设置缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <param name="timeSpan">过期时间.</param>
    /// <returns></returns>
    Task<bool> SetAsync(string key, object value, TimeSpan timeSpan);

    /// <summary>
    /// 获取指定 key 的增量值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="incrBy">增量.</param>
    /// <returns></returns>
    long Incrby(string key, long incrBy);

    /// <summary>
    /// 获取指定 key 的增量值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="incrBy">增量.</param>
    /// <returns></returns>
    Task<long> IncrbyAsync(string key, long incrBy);

    /// <summary>
    /// 获取缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    string Get(string key);

    /// <summary>
    /// 获取缓存.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    Task<string> GetAsync(string key);

    /// <summary>
    /// 获取缓存.
    /// </summary>
    /// <typeparam name="T">对象.</typeparam>
    /// <param name="key">键.</param>
    /// <returns></returns>
    T Get<T>(string key);

    /// <summary>
    /// 获取缓存.
    /// </summary>
    /// <typeparam name="T">对象.</typeparam>
    /// <param name="key">键.</param>
    /// <returns></returns>
    Task<T> GetAsync<T>(string key);

    /// <summary>
    /// 获取缓存过期时间.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    DateTime GetCacheOutTime(string key);

    /// <summary>
    /// 检查给定 key 是否存在.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    bool Exists(string key);

    /// <summary>
    /// 异步检查给定 key 是否存在.
    /// </summary>
    /// <param name="key">键.</param>
    /// <returns></returns>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// 只有在 key 不存在时设置 key 的值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    /// <param name="expire">有效期.</param>
    bool SetNx(string key, object value, TimeSpan expire);

    /// <summary>
    /// 只有在 key 不存在时设置 key 的值.
    /// </summary>
    /// <param name="key">键.</param>
    /// <param name="value">值.</param>
    bool SetNx(string key, object value);
}