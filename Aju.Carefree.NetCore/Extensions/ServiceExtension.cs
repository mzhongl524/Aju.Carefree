using Aju.Carefree.NetCore.Helpers;
using Aju.Carefree.NetCore.IOC;
using AspectCore.Configuration;
using CSRedis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aju.Carefree.NetCore.Extensions
{
    /// <summary>
    /// IServiceCollection扩展
    /// </summary>
    public static class ServiceExtension
    {
        public static IServiceCollection AddTransientAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            if (string.IsNullOrEmpty(implementAssemblyName))
                throw new ArgumentNullException(nameof(implementAssemblyName));

            var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //过滤掉非接口及泛型接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                //过滤掉抽象类、泛型类以及非class
                var implementType = implementAssembly.DefinedTypes
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                                         t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    service.AddTransient(type, implementType.AsType());
                }
            }

            return service;
        }
        public static IServiceCollection AddSingletonAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            if (string.IsNullOrEmpty(implementAssemblyName))
                throw new ArgumentNullException(nameof(implementAssemblyName));

            var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //过滤掉非接口及泛型接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                //过滤掉抽象类、泛型类以及非class
                var implementType = implementAssembly.DefinedTypes
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                                         t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    service.AddSingleton(type, implementType.AsType());
                }
            }

            return service;
        }

        public static IServiceCollection AddScopedAssembly(this IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(interfaceAssemblyName))
                throw new ArgumentNullException(nameof(interfaceAssemblyName));
            if (string.IsNullOrEmpty(implementAssemblyName))
                throw new ArgumentNullException(nameof(implementAssemblyName));

            var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
            if (interfaceAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
            }

            var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
            if (implementAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
            }

            //过滤掉非接口及泛型接口
            var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

            foreach (var type in types)
            {
                //过滤掉抽象类、泛型类以及非class
                var implementType = implementAssembly.DefinedTypes
                    .FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
                                         t.GetInterfaces().Any(b => b.Name == type.Name));
                if (implementType != null)
                {
                    service.AddScoped(type, implementType.AsType());
                }
            }

            return service;
        }

        public static IServiceCollection RegisterControllers(this IServiceCollection service,
        string controllerAssemblyName)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (string.IsNullOrEmpty(controllerAssemblyName))
                throw new ArgumentNullException(nameof(controllerAssemblyName));
            var controllerAssembly = RuntimeHelper.GetAssembly(controllerAssemblyName);
            if (controllerAssembly == null)
            {
                throw new DllNotFoundException($"the dll \"{controllerAssemblyName}\" not be found");
            }

            //过滤掉非接口及泛型接口
            var types = controllerAssembly.GetTypes().Where(t =>
            {
                var typeInfo = t.GetTypeInfo();
                return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType && t.IsAssignableFrom(typeof(Controller));
            });

            foreach (var type in types)
            {
                service.AddScoped(type);
            }

            return service;
        }

        /// <summary>
        /// 使用CSRedis代替StackChange.Redis
        /// <remarks>
        /// 关于CSRedis项目，请参考<seealso cref="https://github.com/2881099/csredis"/>
        /// </remarks>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="redisConnectionStrings">redis连接字符串。
        /// <remarks>
        /// 如果是单机模式，请只输入一个连接字符串；如果是集群模式，请输入多个连接字符串
        /// </remarks>
        /// </param>
        /// <returns></returns>

        public static IServiceCollection UseCsRedisClient(this IServiceCollection services, params string[] redisConnectionStrings)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (redisConnectionStrings == null || redisConnectionStrings.Length == 0)
            {
                throw new ArgumentNullException(nameof(redisConnectionStrings));
            }
            CSRedisClient redisClient;
            if (redisConnectionStrings.Length == 1)
            {
                //单机模式
                redisClient = new CSRedisClient(redisConnectionStrings[0]);
            }
            else
            {
                //集群模式
                redisClient = new CSRedisClient(NodeRule: null, connectionStrings: redisConnectionStrings);
            }
            //初始化 RedisHelper
            RedisHelper.Initialization(redisClient);
            //注册mvc分布式缓存
            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));
            return services;
        }

        public static IServiceProvider BuildAspectCoreServiceProvider(this IServiceCollection services,
            Action<IAspectConfiguration> configure = null)
        {
            return AspectCoreContainer.BuildServiceProvider(services, configure);
        }


        /// <summary>
        /// 添加自定义Controller。自定义controller项目对应的dll必须复制到程序运行目录
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="controllerAssemblyName">自定义controller文件的名称，比如：xxx.Controllers.dll</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IMvcBuilder AddCustomController(this IMvcBuilder builder, string controllerAssemblyName,
            Func<TypeInfo, bool> filter = null)
        {
            if (filter == null)
                filter = m => true;
            return builder.ConfigureApplicationPartManager(m =>
            {
                var feature = new ControllerFeature();
                m.ApplicationParts.Add(new AssemblyPart(Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + controllerAssemblyName)));
                m.PopulateFeature(feature);
                builder.Services.AddSingleton(feature.Controllers.Where(filter).Select(t => t.AsType()).ToArray());
            });
        }

        /// <summary>
        /// 添加自定义Controller
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="controllerAssemblyDir">Controller文件所在路径</param>
        /// <param name="controllerAssemblyName">Controller文件名称，比如：xxx.Controllers.dll</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IMvcBuilder AddCustomController(this IMvcBuilder builder, string controllerAssemblyDir, string controllerAssemblyName,
            Func<TypeInfo, bool> filter = null)
        {
            if (filter == null)
                filter = m => true;
            return builder.ConfigureApplicationPartManager(m =>
            {
                var feature = new ControllerFeature();
                m.ApplicationParts.Add(
                    new AssemblyPart(Assembly.LoadFile(Path.Combine(controllerAssemblyDir, controllerAssemblyName))));
                m.PopulateFeature(feature);
                builder.Services.AddSingleton(feature.Controllers.Where(filter).Select(t => t.AsType()).ToArray());
            });
        }
    }
}
