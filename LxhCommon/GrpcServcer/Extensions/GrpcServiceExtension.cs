using LxhCommon.GrpcServcer.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Reflection;

namespace LxhCommon.GrpcServcer.Extensions
{
    public static class GrpcServiceExtension
    {
        public static void Add_Grpc_Services(IEndpointRouteBuilder builder,string nameSpace)
        {
            List<Assembly> assemblies = new List<Assembly>();
            if (nameSpace != null)
            {
                assemblies=Assemblies.AllAssemblies.Where(it => it.FullName.StartsWith(nameSpace)).ToList();
                
            }
            else
            {
                assemblies = Assemblies.AllAssemblies.ToList();
            }
            foreach (Assembly assembly in assemblies)
            {
                foreach (var item in GrpcServicesHelper.GetGrpcServices(assembly.FullName))
                {
                    Type mytype = assembly.GetType(item.Value + "." + item.Key);
                    var method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService").MakeGenericMethod(mytype);
                    method.Invoke(null, new[] { builder });
                }
            }
        }

        public static void useMyGrpcServices(this IApplicationBuilder app,string nameSpace)
        {
            //确保grpc启动前UseRouting
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                Add_Grpc_Services(endpoints, nameSpace);
            });
        }
    }
}
