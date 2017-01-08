using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;

namespace WebAPIApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var cert = new X509Certificate2("webapi.pfx", "P@ssword1");

            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                .Build();
            
            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel(options => {
                    options.UseHttps("webapi.pfx", "P@ssword1");
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:5000", "https://*:5001")
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
