using System;
using System.Linq;
using System.Reflection;
using Aju.Carefree.AutoMapperConfig;
using Aju.Carefree.Common;
using Aju.Carefree.Common.DataBaseCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            //数据库连接字符串
            DbFactory.DbConnectionString = _configuration.GetConnectionString("DefaultConnection");

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

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
