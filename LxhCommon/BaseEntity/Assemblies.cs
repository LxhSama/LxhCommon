using LxhCommon.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using static Microsoft.AspNetCore.Builder.AppWebApplicationBuilderExtensions;

namespace LxhCommon;

/// <summary>
/// 全局应用类
/// </summary>
public static class Assemblies
{
    public static Options options => AppWebApplicationBuilderExtensions.options;

    /// <summary>
    /// 应用有效程序集
    /// </summary>
    public static readonly IEnumerable<Assembly> AllAssemblies;

    /// 可访问程序集
    /// </summary>
    public static readonly IEnumerable<Type> PublicTypes;

    /// <summary>
    /// 构造函数
    /// </summary>
    static Assemblies()
    {
        // 加载程序集
        AllAssemblies = GetAssemblies();
        // 获取可访问程序集
        PublicTypes = AllAssemblies.SelectMany(u => u.GetTypes()
            .Where(u => u.IsPublic));
    }


    /// <summary>
    /// 获取应用有效程序集
    /// </summary>
    /// <returns>IEnumerable</returns>
    private static IEnumerable<Assembly> GetAssemblies()
    {
        //https://docs.microsoft.com/zh-cn/dotnet/api/microsoft.extensions.dependencymodel.dependencycontext?view=dotnet-plat-ext-6.0 文档地址
        //获得全部依赖合集
        var dependencyContext = DependencyContext.Default;
        IEnumerable<Assembly> result;
        if (string.IsNullOrEmpty(options.NameSpace))
        {
            result = dependencyContext.RuntimeLibraries.Where(d => d.Type == "project" && !options.ExcludeAssemblyNames.Any(j => d.Name.EndsWith(j)))
                                                       .Select(d => Reflect.GetAssembly(d.Name));//根据名称找到对应程序集这里不包含包因为不过滤
        }
        else
        {
            result = dependencyContext.RuntimeLibraries.Where(d => d.Type == "project" || (d.Type=="package" && d.Name.StartsWith(options.NameSpace)))
                                                       .Select(d => Reflect.GetAssembly(d.Name));//根据名称找到对应程序集且包必须是自己的
        }
        return result;
    }
}
