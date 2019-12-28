using CommandLine;
using CommandLine.Text;
using FroniusSolarClient.Entities.SolarAPI.V1;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using System.Collections.Generic;

namespace FroniusSolarApi.Poller.CLI
{
    [Verb("fetchPowerFlow", HelpText = "Provides detailed information about the local energy grid.")]
    internal class FetchPowerFlowRealtimeDataOptions : Options
    {
        [Option('s', longName: "Store", HelpText = "Where to save the data.", Required = true)]
        public DataStore Store { get; set; }

        [Usage(ApplicationAlias = "poller")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                new Example("Fetch GetPowerFlowRealtimeData and output to the console", new FetchPowerFlowRealtimeDataOptions { Store = DataStore.Console })
            };
            }
        }
    }
}
