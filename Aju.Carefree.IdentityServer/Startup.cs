using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Aju.Carefree.IdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //添加IdentityServer4 服务
            services.AddIdentityServer()
                /*对于Token签名需要一对公钥和私钥,
                 不过IdentityServer为开发者提供了一个AddDeveloperSigningCredential()方法，它会帮我们搞定这个事，并默认存到硬盘中。当切换到生产环境时，还是得使用正儿八经的证书，更换为使用AddSigningCredential()方法。
                */
                .AddDeveloperSigningCredential()
                //配置身份资源
                .AddInMemoryIdentityResources(Config.GetIdentityResource())
                //配置允许验证的Client
                .AddInMemoryClients(Config.GetClients())
                //配置Api资源
                .AddInMemoryApiResources(Config.GetApiResources())
                //配置 测试用户信息
                .AddTestUsers(Config.GetUsers());

            services.AddAuthentication("Bearer")
                .AddCookie("Cookies")
                .AddJwtBearer("Bearer", options =>
                {
                    //identityserver4 地址 也就是本项目地址
                    options.Authority = "http://localhost:16250";
                    options.RequireHttpsMetadata = false;
                    options.Audience = "api1";
                });
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
