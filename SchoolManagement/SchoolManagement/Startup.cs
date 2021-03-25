using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SchoolManagement
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("從第一個Middleware的Hello world");
            //});

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("從第二個Middleware的Hello world");
            //});


            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW1:傳入請求");
                await next();
                logger.LogInformation("MW1:回應請求");
            });

            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW2:傳入請求");
                await next();
                logger.LogInformation("MW2:回應請求");

            });

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("MW3:處理請求並生成回應");
                logger.LogInformation("MW3:處理請求並生成回應");
            });

        }
    }
}
