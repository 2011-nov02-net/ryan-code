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
            //for configuring middleware before plugging in and adding services to the DI container
            //services.AddControllers(); //just controllers
            services.AddControllersWithViews(); //controllers and views
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
                        html = File.ReadAllText("./wwwroot" + filepath + ".html");
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
                        html = File.ReadAllText("./wwwroot" + filepath + ".html");
                    }
                    catch(FileNotFoundException e)
                    {
                        context.Response.Redirect("/");
                    }

                    await context.Response.WriteAsync(html);
                });

                //asp net core mvc
                //every route definition needs a controller and an action on that controller
                endpoints.MapControllerRoute(
                   name: "hello-controller",
                   pattern: "hello",
                   defaults: new { controller = "Hello", action = "Action1" });

                //with parmams
                endpoints.MapControllerRoute(
                   name: "hello-controller2",
                   pattern: "hello/{param1}/{param2:int?}",
                   defaults: new { controller = "Hello", action = "ParameterAction1"});

                //better way
                //paramerter named action will determine action so dont need different routes like above
                endpoints.MapControllerRoute(
                   name: "hello-controller-gerneic",
                   pattern: "hi/{action=Action1}/{param1}/{param2:int?}",
                   defaults: new { controller = "Hello"});
                
                //for views
                endpoints.MapControllerRoute(
                   name: "view-controller-gerneic",
                   pattern: "viewbased/{action=Home}",
                   defaults: new { controller = "ViewBased" });
            });
        }
    }
}
