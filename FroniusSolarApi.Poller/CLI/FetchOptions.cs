using CommandLine;
using CommandLine.Text;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using System.Collections.Generic;

namespace FroniusSolarApi.Poller.CLI
{
    [Verb("fetch", HelpText = "Retrieve data from the Solar API and save the response to a data store.")]
    internal class FetchOptions : Options
    {
        [Option('c', longName: "collections", HelpText = "Data collections to retrieve.", Required =true)]
        public DataCollection Collections { get; set; }

        [Option('s', longName: "store", HelpText = "Where to save the data", Required = true)]
        public DataStore Store { get; set; }


        [Usage(ApplicationAlias = "poller")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                new Example("Fetch CumulationInverterData collection and output to the console", new FetchOptions { Collections = DataCollection.CumulationInverterData,  Store = DataStore.Console })
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
