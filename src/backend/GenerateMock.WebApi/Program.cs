using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenerateMock.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<KestrelServerOptions>(
                    context.Configuration.GetSection("Kestrel"));
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(IPAddress.Any, 8010, listenOptions =>
                    {
                        listenOptions.UseConnectionLogging();
                    });
                    /*serverOptions.Listen(IPAddress.Loopback, 5001,
                        listenOptions =>
                        {
                            listenOptions.UseHttps("testCert.pfx",
                                "testPassword");
                        });*/
                    // Set properties and call methods on options
                }).UseStartup<Startup>();

            })
            .UseDefaultServiceProvider(options => options.ValidateScopes = false);
        }
    }
}
