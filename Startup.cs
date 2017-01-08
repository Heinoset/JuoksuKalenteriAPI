using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger;
using Swashbuckle.SwaggerGen;

namespace WebAPIApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {            
            // Add framework services.
            services.AddMvc(); 
                        
            // Add cors support       
            services.AddCors(options => 
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials() 
                );
            });

            // Add swaggerGen for JSON support
            services.AddSwaggerGen(options => {
                options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info{
                    Version="v1",
                    Title="Juoksukalenteri",
                    Description="Juoksukalenterin testiAPI",
                    TermsOfService = "None"
                });
            });

            // Add services required for using options
            services.AddOptions(); 
            
            // Register the IConfiguration instance which MyOptions binds against
            services.Configure<JuoksukalenteriApiSettings>(options => Configuration.GetSection("JuoksukalenteriApiSettings").Bind(options));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseCors("CorsPolicy");// Shows UseCors with CorsPolicyBuilder.

            app.UseSwagger(); /*Enabling swagger file*/
            app.UseSwaggerUi(); /*Enabling Swagger ui, consider doing it on Development env only*/

            app.UseMvc();
        }
    }
}
