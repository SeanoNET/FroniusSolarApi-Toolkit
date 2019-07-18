using CommandLine;
using CommandLine.Text;
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
                //with.AutoHelp = true;
                //with.AutoVersion = true;
                with.HelpWriter = null; //Console.Out;
            });
            var result = parser.ParseArguments<FetchOptions, object>(args);
            return result.MapResult(
                        (FetchOptions opts) => poller.FetchAndSave(opts),
                    errs => {
                        var helpText = HelpText.AutoBuild(result, h =>
                        {
                            // Configure HelpText	 
                            h.AddEnumValuesToHelpText = true;
                            return h;
                        }, e => e);

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
