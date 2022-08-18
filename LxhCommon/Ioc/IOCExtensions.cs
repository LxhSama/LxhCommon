using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LxhCommon.IOC
{
    public static class IocExtensions
    {
        /// <summary>
        /// 注册引用程序域中所有有AppService标记的类的服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddIOC(this IServiceCollection services)
        {
            string nameSpace = AppWebApplicationBuilderExtensions.options.IOCSpace;
            List<Assembly> assemblies = new List<Assembly>();
            if (nameSpace != null)
            {
                assemblies = Assemblies.AllAssemblies.Where(it => it.FullName.StartsWith(nameSpace)).ToList();

            }
            else
            {
                assemblies = Assemblies.AllAssemblies.ToList();
            }
            foreach (Assembly assembly in assemblies)
            {
                Register(services, assembly);
            }
        }

        private static void Register(IServiceCollection services, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var serviceAttribute = type.GetCustomAttribute<IOCServiceAttribute>();

                if (serviceAttribute != null)
                {
                    var serviceType = serviceAttribute.ServiceType;
                    //情况1 适用于依赖抽象编程，注意这里只获取第一个
                    if (serviceType == null && serviceAttribute.InterfaceServiceType)
                    {
                        serviceType = type.GetInterfaces().FirstOrDefault();
                    }
                    //情况2 不常见特殊情况下才会指定ServiceType，写起来麻烦
                    if (serviceType == null)
                    {
                        serviceType = type;
                    }

                    switch (serviceAttribute.ServiceLifetime)
                    {
                        case LifeTime.Singleton:
                            services.AddSingleton(serviceType, type);
                            break;
                        case LifeTime.Scoped:
                            services.AddScoped(serviceType, type);
                            break;
                        case LifeTime.Transient:
                            services.AddTransient(serviceType, type);
                            break;
                        default:
                            services.AddTransient(serviceType, type);
                            break;
                    }
                }
            }
        }
    }
}
