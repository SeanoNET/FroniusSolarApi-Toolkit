using CommandLine;
using CommandLine.Text;
using FroniusSolarClient.Entities.SolarAPI.V1;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using System.Collections.Generic;

namespace FroniusSolarApi.Poller.CLI
{
    [Verb("fetch", HelpText = "Obtain data from various Fronius devices (inverters, SensorCards, StringControls) via the Solar API and save to a data store.")]
    internal class FetchInverterRealtimeDataOptions : Options
    {
        [Option('c', longName: "collections", HelpText = "Data collections to retrieve.", Required =true)]
        public DataCollection Collections { get; set; }

        [Option('s', longName: "store", HelpText = "Where to save the data.", Required = true)]
        public DataStore Store { get; set; }

        [Option('p', longName: "scope", HelpText = "Query specific device(s) or whole system.", Required = false, Default = Scope.Device)]
        public Scope Scope { get; set; }

        [Option('d', longName: "device", HelpText = "The device id to query.", Required = false, Default = 1)]
        public int DeviceId { get; set; }

        [Usage(ApplicationAlias = "poller")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                new Example("Fetch CumulationInverterData collection and output to the console", new FetchInverterRealtimeDataOptions { Collections = DataCollection.CumulationInverterData,  Store = DataStore.Console })
            };
            }
        }
    }
    // TODO: Output supported data stores from repository
    internal enum DataStore
    {
        Console,
        Csv,
        Mssql
    }
}
