using LxhCommon.Cache;
using Yitter.IdGenerator;

namespace LxhCommon;

public static class IdHelper
{
    private static string caCheKey = string.Empty;

    /// <summary>
    /// 机器码位长.
    /// </summary>
    private static byte workerIdBitLength = 16;

    /// <summary>
    /// 32767.
    /// </summary>
    private static int maxWorkerIdNumberByMode = (1 << workerIdBitLength) - 1 > short.MaxValue ? short.MaxValue : (1 << workerIdBitLength) - 1;

    /// <summary>
    /// 机器码.
    /// </summary>
    private static ushort workerId = 0;

    /// <summary>
    /// 缓存管理.
    /// </summary>

    private static ICacheManager _cacheManager;
    static IdHelper()
    {
        _cacheManager = new CacheManager(new RedisCache());
    }

    /// <summary>
    /// 初始化雪花生成器WorkerID， 通过Redis实现集群获取不同的编号， 如果相同会出现ID重复.
    /// </summary>
    public static async void initIdWorker()
    {
            for (int i = 0; i < workerIdBitLength; i++)
            {
                long andInc = _cacheManager.Incrby("snow", 1);
                long result = andInc % (maxWorkerIdNumberByMode + 1);

                // 计数超出上限之后重新计数
                if (andInc >= maxWorkerIdNumberByMode)
                {
                    _cacheManager.Set("snow", andInc % maxWorkerIdNumberByMode);
                }

                caCheKey = "workerId" + result;

                if (_cacheManager.SetNx(caCheKey, string.Empty, TimeSpan.FromDays(1)))
                {
                    workerId = (ushort)result;
                    break;
                }
            }

            if (workerId == 0)
            {
                throw new Exception("已尝试生成{0}个ID生成器编号, 无法获取到可用编号");
            }

        YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = workerId, WorkerIdBitLength = workerIdBitLength });
        await ResetExpire(caCheKey);
    }

    /// <summary>
    /// 每30分续约一次雪花Id机器码.
    /// </summary>
    private static async Task ResetExpire(string caCheKey)
    {
        while (true)
        {
            await Task.Run(() => {
                _cacheManager.SetAsync(caCheKey, string.Empty, TimeSpan.FromDays(1));
                Task.Delay(1800000);
            });
        }
    }

    /// <summary>
    /// 生成ID.
    /// </summary>
    /// <returns></returns>
    public static string NextId()
    {
        return YitIdHelper.NextId().ToString();
    }
}
