using BusinessLogic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared;
using Shared.ChannelEngineRestClient;
using ChannelEngineSample.Console.Services;
using Serilog;
using System;

namespace ChannelEngineSample.Console

{
    class Program
    {
        
        public static async Task Main(string[] args)
        {
        
            //Add logging using Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(@"log-.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
            var host = CreateHostBuilder(args)
                .UseSerilog()
                .Build();
            Serilog.Debugging.SelfLog.Enable(s => System.Console.WriteLine($"Internal Error with Serilog: {s}"));
            var myService = host.Services.GetRequiredService<ApiConsumerService>();
            await myService.Demonstrate();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var configurationRoot = context.Configuration;
                    services
                        .Configure<ChannelEngineApiConfig>(
                            configurationRoot.GetSection("ChannelEngineApiConfig"))
                        .AddSingleton<IChannelEngineRestClient, ChannelEngineRestClient>()
                        .AddSingleton<Orders>()
                        .AddSingleton<Products>()
                        .AddTransient<ApiConsumerService>();
                });
    }
}