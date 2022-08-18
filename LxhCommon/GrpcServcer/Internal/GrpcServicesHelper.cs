using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LxhCommon.GrpcServcer.Internal
{
    public class GrpcServicesHelper
    {
        public static Dictionary<string, string> GetGrpcServices(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<string, string>();
                foreach (var item in ts.Where(u => u.CustomAttributes.Any(a => a.AttributeType.Name == "GrpcServiceAttribute")))
                {
                    result.Add(item.Name, item.Namespace);
                }

                return result;
            }

            return new Dictionary<string, string>();
        }
    }
}
