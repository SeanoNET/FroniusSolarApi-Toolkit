using CommandLine;
using CommandLine.Text;
using FroniusSolarApi.Poller.CLI;
using FroniusSolarClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace FroniusSolarApi.Poller
{
    partial class Program
    {
        private static void ConfigureServices(IServiceCollection services)
        {
            //configure logging here
            services.AddLogging(configure => configure.AddConsole())
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

            var parser = new Parser(with => {
                with.CaseInsensitiveEnumValues = true;
                //with.AutoHelp = true;
                //with.AutoVersion = true;
                with.HelpWriter = null; //Console.Out;
            });
            var result = parser.ParseArguments<FetchInverterRealtimeDataOptions, object>(args);
            return result.MapResult(
                        (FetchInverterRealtimeDataOptions opts) => poller.FetchAndSaveInverterRealtimeData(opts),
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

            //RepositoryService service = new RepositoryService(new CsvRepository(new CsvConfiguration(@"C:\Temp","test")));

            //var solarClient = new SolarClient("10.1.1.124", 1, OutputResponseHeader);

            //var data = solarClient.GetCommonInverterData();

            //service.SaveData(data);
        }
    }
}
