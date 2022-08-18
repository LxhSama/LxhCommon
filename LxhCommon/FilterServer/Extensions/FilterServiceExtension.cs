using LxhCommon.FilterServer.Internal;
using LxhCommon.GrpcServcer.Internal;
using LxhCommon.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LxhCommon.GrpcServcer.Extensions;

public static class FilterServiceExtension
{
    public static IServiceCollection AddAllFilter(this IServiceCollection services)
    {
        services.Configure<MvcOptions>(options =>
        {
            foreach (Type t in FilterHelper.GetFilteres())
            {
                options.Filters.Add(t,Convert.ToInt32(t.GetModelValue("Order")));
            }
        });
        return services;
    }
}


