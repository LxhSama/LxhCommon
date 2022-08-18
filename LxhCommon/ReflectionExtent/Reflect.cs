using System.Reflection;
using System.Runtime.Loader;

namespace LxhCommon.Reflection;

/// <summary>
/// 内部反射静态类
/// </summary>
internal static class Reflect
{
    /// <summary>
    /// 获取入口程序集
    /// </summary>
    /// <returns></returns>
    internal static Assembly GetEntryAssembly()
    {
        return Assembly.GetEntryAssembly();
    }
    /// <summary>
    /// 获取程序集名称(根据类)
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    internal static string GetAssemblyName(Type type)
    {
        return GetAssemblyName(type.GetTypeInfo());
    }

    /// <summary>
    /// 获取程序集名称
    /// </summary>
    /// <param name="typeInfo"></param>
    /// <returns></returns>
    internal static string GetAssemblyName(TypeInfo typeInfo)
    {
        return GetAssemblyName(typeInfo.Assembly);
    }
    /// <summary>
    /// 获取程序集名称
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    internal static string GetAssemblyName(Assembly assembly)
    {
        return assembly.GetName().Name;
    }
    /// <summary>
    /// 根据程序集名称获取运行时程序集
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <returns></returns>
    internal static Assembly GetAssembly(string assemblyName)
    {
        // 加载程序集
        return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
    }

    /// <summary>
    /// 根据路径加载程序集
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    internal static Assembly LoadAssembly(string path)
    {
        if (!File.Exists(path)) return default;
        return Assembly.LoadFrom(path);
    }




}