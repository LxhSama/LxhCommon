# LXHCommon说明
## 整体说明
 1.该包是一个包含各类通用文件和方法的库，目前集成有:
  																			IOC扫描注入，
																			 	Filter扫描注入，
 																				GRPC类自动注入。
 帮助类包含一个反射帮助类，以及一个全局事务filter。
 后续会持续补充各种用法和方法。该包不仅包含架构层的内容还有类似文件上传，LINQ扩展等等。
 只要我想到可以公用的轮子都可能会放上, 欢迎大家提供轮子需要和思路
 github地址：https://github.com/LxhSama/LxhCommon 
 ## 使用方法
 1.Install-Package LxhCommon 使用最新版（目前是1.0）
 该库适用在.net6的项目中，如果是webapi项目,入口时提供的builder和WebApplication可作为调用方法.
 如果是在非WEBAPi项目中，可使用的为公共库，如反射的帮助方法。
 2.代码使用
``` c#
 			builder.AddLxhCommonServer(opts => 
{
    opts.UseAll = true; //是否注入全部服务，已有的IOC，FIlter注入，GPPC注入等
    opts.UseGrpcServer = false;//单独开启GRPC注入
    opts.UseAllFilter = true;//单独开启过滤器注入，这里需要注意的点见注意点2
    opts.UseIOC = true;//单独开启IOC注入，用法见注意点1
    opts.WebPort = 9998;//web端口
    opts.GrpcPort = 9999;//grpc端口
    opts.NameSpace = null;//不为null则只取以该字段开头的命名空间
    opts.FilterSpace = null;//在总过滤后为加载Filter单独过滤命名空间
    opts.GrpcSpace = null;//在过滤后为加载Grpc单独过滤命名空间
    opts.IOCSpace = null;//在过滤后为加载IOC单独过滤命名空间
});
//目前没有顺序要求不排除未来没有,目前只对GRPC注入时有要求使用，不打开grpc服务不需要使用该方法。后续有用到会修改说明.
app.run前使用app.UseLxhCommon()//见注意点3

```
具体使用实例请查看github。
 ## 注意点
 1.单独的类，标注特性 [IOCService] 即可,不指定也会有默认作用范围.接口的实现类上标注 [IOCService(ServiceType= typeof(IType))] 即可完成实现类到接口的IOC注入。

 2.Filter必须继承自IMyAsyncActionFilter,IMyAsyncAuthorizationFilter,IMyAsyncExceptionFilter,IMyAsyncResultFilter其中的一种才会进行注入。用法与微软官方的分类相同。接口中有一个Order属性必须实现，可指定为0，如果需要排序，使用Order值改变执行顺序

 3.UseLxhCommon里使用了app.UseRouting()
