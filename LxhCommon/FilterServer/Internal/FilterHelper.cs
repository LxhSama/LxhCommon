using LxhCommon.BaseEntity;
using LxhCommon.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Builder.AppWebApplicationBuilderExtensions;

namespace LxhCommon.FilterServer.Internal
{
    public static class FilterHelper
    {
        public static Options options => AppWebApplicationBuilderExtensions.options;
        public static List<Type> GetFilteres()
        {
            string nameSpace = options.FilterSpace;
            List<Assembly> assemblies = new List<Assembly>();
            if (nameSpace != null)
            {
                assemblies = Assemblies.AllAssemblies.Where(it => it.FullName.StartsWith(nameSpace)).ToList();

            }
            else
            {
                assemblies = Assemblies.AllAssemblies.ToList();
            }
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                Type[] t=assembly.GetTypes();
                types.AddRange(t.Where(t => t.IsBaseOn<IFilter>() && t.IsClass));
            }
            return types;
        }
    }
}
