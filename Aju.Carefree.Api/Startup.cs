using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Aju.Carefree.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Elasticsearch
            //添加单例依赖注入
            services.AddSingleton<IEsClientProvider, EsClientProvider>();
            #endregion

            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:16250";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";
                });

            //Swagger
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Title = "标题-Aju",
                        Version = "版本-V1",
                        Description = "Api文档说明",
                        TermsOfService = "服务条件",
                        Contact = new Contact() { Email = "1022560838@qq.com", Name = "Aju", Url = "https://github.com/AjuPrince/Aju.Carefree" },
                        License = new License { Name = "", Url = "" }
                    });
                });
            //添加Xml注释功能
            services.AddSwaggerGen(c =>
                {
                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Aju.Carefree.Api.xml"));
                });
            //services.AddMvcCore().AddApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger(c =>
            {
            });
            app.UseSwaggerUI(c =>
            {
                //c.ShowExtensions();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "test V1");
            });
        }
    }
}
