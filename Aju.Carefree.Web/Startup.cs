using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Cache;
using Aju.Carefree.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
namespace Aju.Carefree.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //添加分布式缓存
            services.AddSingleton(typeof(ICacheService), new RedisCacheService(new Microsoft.Extensions.Caching.Redis.RedisCacheOptions
            {
                Configuration = Configuration.GetSection("Cache:ConnectionCacheStr").Value,
                InstanceName = Configuration.GetSection("Cache:CacheInstanceName").Value
            }));

            #region AutoMapper
            //services
            //services.AddAutoMapper();
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            //  app.UseCookiePolicy();

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
