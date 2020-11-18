using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HelloAspNetCore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/";
                    await next();
                }
            });

            //app.UseStaticFiles(); //use html files in wwwroot folder
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string filepath = "/index";
                    string html = "";

                    try
                    {
                        html = File.ReadAllText("./Pages" + filepath + ".html");
                    }
                    catch (FileNotFoundException e)
                    {
                        await context.Response.WriteAsync("Home Page");
                    }

                    await context.Response.WriteAsync(html);
                });
                endpoints.MapGet("/htmlpage", async context =>
                {
                    string filepath = context.Request.Path;
                    string html = "";
                    
                    try
                    {
                        html = File.ReadAllText("./Pages" + filepath + ".html");
                    }
                    catch(FileNotFoundException e)
                    {
                        context.Response.Redirect("/");
                    }

                    await context.Response.WriteAsync(html);
                });
            });
        }
    }
}
