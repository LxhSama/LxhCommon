//using Yitter.IdGenerator;

//namespace LxhCommon;

//public class SnowflakeIdHelper
//{
//    private static string caCheKey = string.Empty;

//    /// <summary>
//    /// 机器码位长.
//    /// </summary>
//    private static byte workerIdBitLength = 16;

//    /// <summary>
//    /// 32767.
//    /// </summary>
//    private static int maxWorkerIdNumberByMode = (1 << workerIdBitLength) - 1 > short.MaxValue ? short.MaxValue : (1 << workerIdBitLength) - 1;

//    /// <summary>
//    /// 机器码.
//    /// </summary>
//    private static ushort workerId = 0;

//    /// <summary>
//    /// 缓存管理.
//    /// </summary>
//    private static IDis _cacheManager = App.GetService<ICacheManager>();

//    /// <summary>
//    /// 初始化雪花生成器WorkerID， 通过Redis实现集群获取不同的编号， 如果相同会出现ID重复.
//    /// </summary>
//    public static async void initIdWorker()
//    {
//            for (int i = 0; i < workerIdBitLength; i++)
//            {
//                long andInc = _cacheManager.Incrby(CommonConst.CACHEKEYSNOWFLAKEID, 1);
//                long result = andInc % (maxWorkerIdNumberByMode + 1);

//                // 计数超出上限之后重新计数
//                if (andInc >= maxWorkerIdNumberByMode)
//                {
//                    _cacheManager.Set(CommonConst.CACHEKEYSNOWFLAKEID, andInc % maxWorkerIdNumberByMode);
//                }

//                caCheKey = CommonConst.CACHEKEYWORKERID + result;

//                if (_cacheManager.SetNx(caCheKey, string.Empty, TimeSpan.FromDays(1)))
//                {
//                    workerId = (ushort)result;
//                    break;
//                }
//            }

//            if (workerId == 0)
//            {
//                throw new Exception("已尝试生成{0}个ID生成器编号, 无法获取到可用编号", maxWorkerIdNumberByMode + 1);
//            }

//        YitIdHelper.SetIdGenerator(new IdGeneratorOptions { WorkerId = workerId, WorkerIdBitLength = workerIdBitLength });
//        ResetExpire(caCheKey);
//    }

//    /// <summary>
//    /// 每30分续约一次雪花Id机器码.
//    /// </summary>
//    private static void ResetExpire(string caCheKey)
//    {
//        Action<SpareTimer, long> action = async (timer, count) =>
//        {
//            await _cacheManager.SetAsync(caCheKey, string.Empty, TimeSpan.FromDays(1));
//        };
//        SpareTime.Do("0 0/30 * * * ?", action, caCheKey, string.Empty, true, executeType: SpareTimeExecuteTypes.Parallel);
//    }

//    /// <summary>
//    /// 生成ID.
//    /// </summary>
//    /// <returns></returns>
//    public static string NextId()
//    {
//        return YitIdHelper.NextId().ToString();
//    }
//}
