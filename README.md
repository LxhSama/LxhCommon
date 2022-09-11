# LXHCommon说明
## 整体说明
 1.该包是一个包含各类通用文件和方法的库，目前集成有:
  																		IOC扫描注入，
																		  Filter扫描注入，
 																		 GRPC类自动注入,

​																		 动态API自动加载.

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
//新增顺序要求，如果开启动态api必须在AddController()后执行
builder.AddLxhCommonServer(opts => 
{
    opts.UseAll = true; //是否注入全部服务，已有的IOC，FIlter注入，GPPC注入等
    opts.UseGrpcServer = false;//单独开启GRPC注入
    opts.UseAllFilter = true;//单独开启过滤器注入，这里需要注意的点见注意点2
    opts.UseIOC = true;//单独开启IOC注入，用法见注意点1
    opts.UseDynamicApi=true;//开启动态APi加载
    opts.UseSwagger = false;
    opts.UseCache = true;//开启内存缓存
    opts.RedisCoon = "";//开启Redis缓存
    //暂时不用
    //opts.WebPort = 9998;//web端口
    //opts.GrpcPort = 9999;//grpc端口
    opts.NameSpace = null;//不为null则只取以该字段开头的命名空间
    opts.FilterSpace = null;//在总过滤后为加载Filter单独过滤命名空间
    opts.GrpcSpace = null;//在过滤后为加载Grpc单独过滤命名空间
    opts.IOCSpace = null;//在过滤后为加载IOC单独过滤命名空间
});
//Swaager已经加入，开启swagger必须调用app.UseLxhCommon()
//目前没有顺序要求不排除未来没有,目前只对GRPC注入时有要求使用，不打开grpc服务不需要使用该方法。后续有用到会修改说明.
app.run前使用app.UseLxhCommon()//见注意点3

```
具体使用实例请查看github。

3.如何使用动态API

​		提供两种使用方法，一种是继承自接口(IDynamicApi)还有一种是打上特性，推荐使用特性(DynamicApi)方式不要使用接口

​		api格式为api/[controllername]/[action] 一般不用打接口谓词标签，遵循约定即可.以下为对应的字典。如方法名GetList开头自动为Get

​        Addxx自动为Post。。。

​        正常写类即可。将该类加入到主项目中，将会自动注入

```c#

            ["get"] = "GET",
            ["find"] = "GET",
            ["fetch"] = "GET",
            ["query"] = "GET",
            ["post"] = "POST",
            ["add"] = "POST",
            ["create"] = "POST",
            ["insert"] = "POST",
            ["submit"] = "POST",
            ["put"] = "POST",
            ["update"] = "POST",
            ["delete"] = "DELETE",
            ["remove"] = "DELETE",
            ["clear"] = "DELETE",
            ["patch"] = "PATCH"
```



 ## 注意点
 1.单独的类，标注特性 [IOCService] 即可,不指定也会有默认作用范围.接口的实现类上标注 [IOCService(ServiceType= typeof(IType))] 即可完成实现类到接口的IOC注入。

 2.Filter必须继承自IMyAsyncActionFilter,IMyAsyncAuthorizationFilter,IMyAsyncExceptionFilter,IMyAsyncResultFilter其中的一种才会进行注入。用法与微软官方的分类相同。接口中有一个Order属性必须实现，可指定为0，如果需要排序，使用Order值改变执行顺序

 3.UseLxhCommon里使用了app.UseRouting()

4.swagger加入了默认的jwt支持，可以直接开启

5.开启缓存，缓存使用方法是注入IMemoryCacheHelper和IDistributedCacheHelper

通用的方法,GetOrCreate[Async]

Remove 源自杨老师最后大项目

另封装了RedisHelper 可以注入ICacheManager使用,具体字段请见项目内

## **卫星**

1.1.5 IOC支持属性注入方式
