using CommandLine;
using FroniusSolarApi.Poller.CLI;
using System;

namespace FroniusSolarApi.Poller
{
    partial class Program
    {
        static int Main(string[] args)
        {
            // Poller app
            var poller = new PollerApp();

            var parser = new Parser(with => {
                with.CaseInsensitiveEnumValues = true;
                with.AutoHelp = true;
                with.AutoVersion = true;
                with.HelpWriter = Console.Out;
            });

            return parser.ParseArguments<FetchOptions, object>(args)
                    .MapResult(
                        (FetchOptions opts) => poller.FetchAndSave(opts),
                    errs => 1);

            //RepositoryService service = new RepositoryService(new CsvRepository(new CsvConfiguration(@"C:\Temp","test")));

            //var solarClient = new SolarClient("10.1.1.124", 1, OutputResponseHeader);

            //var data = solarClient.GetCommonInverterData();

            //service.SaveData(data);
        }
    }
}
