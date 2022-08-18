using LxhCommon.GrpcServcer.Extensions;
using LxhCommon.IOC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder;

public static class AppWebApplicationBuilderExtensions
{
    public static Options options;
    public static WebApplicationBuilder AddLxhCommonServer(this WebApplicationBuilder builder, Action<Options> opts = null)
    {
        options = new Options();
        if (options == null)
        {
            options.UseAll = true;
        }
        else
        {
            opts.Invoke(options);
        }
        if (options.UseAll || options.UseIOC)
        {
            builder.Services.AddIOC();
        }
        if (options.UseAll || options.UseAllFilter)
        {
            builder.Services.AddAllFilter();
        }
        if (options.UseAll || options.UseGrpcServer)
        {
            //grpc模块
            builder.Services.AddGrpc();
            //默认指定grpc为9090端口
            builder.WebHost.UseKestrel(options =>
            {
                //webapi监听5000
                options.Listen(System.Net.IPAddress.Any, AppWebApplicationBuilderExtensions.options.WebPort, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                });
                //grpc到9090端口
                options.Listen(System.Net.IPAddress.Any, AppWebApplicationBuilderExtensions.options.GrpcPort, listenOptions =>
                {
                    listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                });
            });
        }
        return builder;
    }
    public static void UseLxhCommon(this IApplicationBuilder app)
    {
        if (options.UseAll || options.UseGrpcServer)
        {
            app.useMyGrpcServices(options.GrpcSpace);
        }
    }

    public class Options
    {
        //开启全部服务
        public bool UseAll = true;
        //开启GRPC服务
        public bool UseGrpcServer = true;
        //开启注册filter服务
        public bool UseAllFilter = true;
        //开启IOC服务
        public bool UseIOC = true;
        //过滤总空间
        public string NameSpace = null;
        //加载grpc时再次过滤
        public string GrpcSpace = null;
        //加载Filter时再次过滤
        public string FilterSpace = null;
        //加载IOC时再次过滤
        public string IOCSpace = null;
        //指定web端口
        public int WebPort = 5000;
        //指定grpc端口
        public int GrpcPort = 9090;
        //过滤末尾命名空间
        public string[] ExcludeAssemblyNames = new string[]{"Default"};
    }
}

