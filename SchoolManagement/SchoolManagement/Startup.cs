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
                var options = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 3
                };
                app.UseDeveloperExceptionPage(options);
            }

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("�q�Ĥ@��Middleware��Hello world");
            //});

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("�q�ĤG��Middleware��Hello world");
            //});
            
            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW1:�ǤJ�ШD");
            //    await next();
            //    logger.LogInformation("MW1:�^���ШD");
            //});

            //app.Use(async (context, next) =>
            //{
            //    logger.LogInformation("MW2:�ǤJ�ШD");
            //    await next();
            //    logger.LogInformation("MW2:�^���ШD");

            //});

            //app.Run(async (context) =>
            //{
            //    context.Response.ContentType = "text/plain;charset=utf-8";
            //    await context.Response.WriteAsync("MW3:�B�z�ШD�åͦ��^��");
            //    logger.LogInformation("MW3:�B�z�ШD�åͦ��^��");
            //});

            //var defaultOptions = new DefaultFilesOptions();
            //defaultOptions.DefaultFileNames.Clear();
            //defaultOptions.DefaultFileNames.Add("custom.html");
            
            //app.UseDefaultFiles(defaultOptions);
            //app.UseStaticFiles();


            //var fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("custom.html");
            //app.UseFileServer(fileServerOptions);

            app.UseFileServer();
            app.Run(async (context) =>
            {
                throw new Exception("�o�Ͳ��`");
                await context.Response.WriteAsync("Hello world");
            });
        }
    }
}
