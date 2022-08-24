using LxhCommon.DynamicApiSimple;
using LxhCommon.IOC;
using Microsoft.AspNetCore.Mvc;
using test1Interface;

namespace test1
{
    [DynamicApi]
    [IOCService(ServiceType = typeof(ITest1))]
    public class Test1 : ITest1
    {
        public string SayHello()
        {
            return "Hello";
        }
        [HttpPost]
        public string GetName([FromBody]n ns)
        {
            return "Hello" + ns.Name;
        }
        [HttpGet("/{id}")]
        public string GetNameById(string id)
        {
            return "Hello" + id;
        }
        public  class n { public string Name { get; set; } }
    }
}