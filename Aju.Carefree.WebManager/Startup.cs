using System;
using System.Linq;
using System.Reflection;
using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Cache;
using Aju.Carefree.Common;
using Aju.Carefree.Common.DapperCore;
using Aju.Carefree.Common.DataBaseCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Aju.Carefree.WebManager
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //SqlSugar数据库连接字符串
            //DbFactory.DbConnectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<DbFactory>();
            //Dapper数据库连接字符串
            DapperHelper.DapperDbConnectionString = _configuration.GetConnectionString("DefaultConnection");
            #region Redis
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(
                new Microsoft.Extensions.Caching.Redis.RedisCacheOptions
                {
                    Configuration = _configuration.GetSection("Cache:ConnectionCacheStr").Value,
                    InstanceName = _configuration.GetSection("Cache:CacheInstanceName").Value
                }));
            //添加分布式缓存
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = _configuration.GetSection("Cache:ConnectionCacheStr").Value;
                option.InstanceName = _configuration.GetSection("Cache:CacheInstanceName").Value;
            });
            #endregion



            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region AutoMapper
            services.AddAutoMapper();
            #endregion

            #region Autofac
            var builder = new ContainerBuilder();//实例化 AutoFac  容器    

            var baseType = typeof(IDependency);
            var assembly = Assembly.Load("Aju.Carefree.Services");
            builder.RegisterAssemblyTypes(assembly)
                  .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                .AsImplementedInterfaces();

            var assemblyR = Assembly.Load("Aju.Carefree.Repositories");
            builder.RegisterAssemblyTypes(assemblyR)
                .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
                .AsImplementedInterfaces();

            builder.Populate(services);

            var applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseStaticFiles();

            //使用NLog记录日志
            loggerFactory.AddNLog();
            //引入Nlog配置文件
            env.ConfigureNLog("nlog.config");


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Mappings.RegisterMappings();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
