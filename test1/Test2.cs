using LxhCommon.DynamicApiSimple;
using LxhCommon.Ioc;
using LxhCommon.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test1Interface;

namespace test1
{
    [IOCService]
    [DynamicApi]

    public class Test2
    {
        [Autowired]
        ITest1 test;
        public Test2(AutowiredService autowiredService)
        {
            autowiredService.Autowired(this);
        }
        public string GetHello()
        {
            return test.SayHello();
        }
    }
}
