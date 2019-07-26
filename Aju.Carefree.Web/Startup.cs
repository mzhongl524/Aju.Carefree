using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Common.DataBaseCore;
using Aju.Carefree.NetCore.Extensions;
using Aju.Carefree.NetCore.IOC;
using Aju.Carefree.Web.Filter;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.Reflection;

namespace Aju.Carefree.Web
{
    public class Startup
    {
        //private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            env.ConfigureNLog("Nlog.config");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //数据库配置
            services.Configure<DbOption>("Aju.Carefree", Configuration.GetSection("DbOption"));
            //Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = "/Account/Index";
            //        options.LogoutPath = "/Account/Logout";
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            //    });
            //Session
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(15);
            //    options.Cookie.HttpOnly = true;
            //});
            // services.Add(ServiceDescriptor.Singleton<ICacheService, RedisCacheService>());

            //services.AddDistributedRedisCache(option =>
            //{
            //    option.Configuration = Configuration.GetSection("Cache")["ConnectionCacheStr"];
            //});



            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);//设置session 的过期时间
            });
            //CSRF
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryKey_Aju";
                options.HeaderName = "X-CSRF-TOKEN-Aju";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddCors((options) =>
            {
                options.AddPolicy("", (b) =>
                {
                    b.WithOrigins(new string[] { "", "", "" }).AllowAnyHeader().AllowAnyMethod();
                });
            });

            //AutoMapper
            services.AddAutoMapper(typeof(CarefreeProfile));
            //mvc
            services.AddMvc(options =>
            {
                options.Filters.Add(new GlobalExceptionFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddControllersAsServices()
            .AddFluentValidation(fv =>
            {
                //去掉其他的验证，只使用FluentValidation的验证规则
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
            //添加数据保护组件
            services.AddDataProtection();

            #region Redis
            var redisConnectionString = Configuration.GetConnectionString("Redis");
            //启用Redis
            services.UseCsRedisClient(redisConnectionString);
            //AspectCoreContainer.Resolve();

            //全局设置Redis缓存有效时间为5分钟。
            //services.Configure<DistributedCacheEntryOptions>(option =>
            //    option.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5));
            #endregion
            //AddMiniProfiler
            services.AddMiniProfiler(options =>
            {
                //设定弹出窗口的位置是左下角
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                //在弹出的明细窗口里会显式Time With Children这列。
                options.PopupShowTimeWithChildren = true;
            });

            //services.AddDistributedServiceStackRedisCache(options =>
            //{
            //    Configuration.GetSection("Cache").Bind(options);
            //});

            #region Autofac
            //var builder = new ContainerBuilder();//实例化 AutoFac  容器    

            //var baseType = typeof(IDependency);
            //var assembly = Assembly.Load("Aju.Carefree.Services");
            //builder.RegisterAssemblyTypes(assembly)
            //                  .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
            //                .AsImplementedInterfaces();

            //var assemblyR = Assembly.Load("Aju.Carefree.Repositories");
            //builder.RegisterAssemblyTypes(assemblyR)
            //                .Where(m => baseType.IsAssignableFrom(m) && m != baseType)
            //                .AsImplementedInterfaces();

            //builder.Populate(services);

            //var applicationContainer = builder.Build();

            //return new AutofacServiceProvider(applicationContainer);

            #endregion

            services.AddSingleton(Configuration)
                .AddScopedAssembly("Aju.Carefree.IRepositories", "Aju.Carefree.Repositories")//注入仓储
                .AddScopedAssembly("Aju.Carefree.IServices", "Aju.Carefree.Services"); //注入服务
            return AspectCoreContainer.BuildServiceProvider(services);//接入AspectCore.Injector
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            //add NLog to ASP.NET Core
            loggerFactory.AddNLog();

            app.UseMiniProfiler();
            app.UseCors();
            Mappings.RegisterMappings();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
