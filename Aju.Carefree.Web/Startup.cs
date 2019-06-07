using Aju.Carefree.Common;
using Aju.Carefree.Common.DataBaseCore;
using Aju.Carefree.Web.Filter;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Index";
                    options.LogoutPath = "/Account/Logout";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                });
            //Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
            });
            //CSRF
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryKey_Aju";
                options.HeaderName = "X-CSRF-TOKEN-Aju";
                options.SuppressXFrameOptionsHeader = false;
            });
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
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
