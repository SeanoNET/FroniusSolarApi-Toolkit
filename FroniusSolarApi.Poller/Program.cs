using CommandLine;
using CommandLine.Text;
using FroniusSolarApi.Poller.CLI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace FroniusSolarApi.Poller
{
    partial class Program
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            // Configure logging
            Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File("logs/pollerlog_.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true))
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information)
                .AddSingleton<PollerApp>()
                .AddSingleton<IConfiguration>(c => ConfigurationBuild());
        }

        private static IConfiguration ConfigurationBuild()
        {
            // Load from configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            return configBuilder.Build();
        }

        static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Poller app
            var poller = serviceProvider.GetService<PollerApp>();

            // Setup CLI parser
            var parser = new Parser(with => {
                with.CaseInsensitiveEnumValues = true;
                with.HelpWriter = null; 
            });

            var result = parser.ParseArguments<FetchInverterRealtimeDataOptions, FetchInverterArchiveDataOptions, object>(args);

            return result.MapResult(
                        (FetchInverterRealtimeDataOptions opts) => poller.FetchAndSaveInverterRealtimeData(opts),
                        (FetchInverterArchiveDataOptions opts) => poller.FetchAndSaveArchiveData(opts),
                    errs => {
                        var helpText = HelpText.AutoBuild(result, h =>
                        {
                            // Configure HelpText	 
                            h.AddEnumValuesToHelpText = true;
                            return h;
                        }, e => e, verbsIndex: true);

                        Console.WriteLine(helpText);
                        return 1;
                    });
        }
    }
}
