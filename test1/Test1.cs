using LxhCommon.IOC;
using test1Model;

namespace test1
{
    [IOCService(ServiceType = typeof(ITest1))]
    public class Test1 : ITest1
    {
        public string sayHello()
        {
            return "Hello";
        }
    }
}