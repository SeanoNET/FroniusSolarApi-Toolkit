using CommandLine;
using CommandLine.Text;
using FroniusSolarClient.Entities.SolarAPI.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace FroniusSolarApi.Poller.CLI
{
    [Verb("fetchArchive", HelpText = "Obtain archive data from various Fronius devices (inverters, SensorCards, StringControls) via the Solar API and save to a data store.")]
    internal class FetchInverterArchiveDataOptions : Options
    {
        [Option('f', longName: "StartDate", HelpText = "Start date of the archive data period.", Required = true)]
        public DateTime StartDate { get; set; }

        [Option('e', longName: "EndDate", HelpText = "End date of the archive data period.", Required = true)]
        public DateTime EndDate { get; set; }

        [Option('c', longName: "Channels", HelpText = "Available data channels to retrieve.", Required = true)]
        [Value(0)]
        public IEnumerable<Channel> Channels { get; set; }

        [Option('s', longName: "Store", HelpText = "Where to save the data.", Required = true)]
        public DataStore Store { get; set; }

        [Option('p', longName: "Scope", HelpText = "Query specific device(s) or whole system.", Required = false, Default = Scope.System)]
        public Scope Scope { get; set; }

        [Option('d', longName: "Device", HelpText = "The device id to query.", Required = false, Default = 1)]
        public int DeviceId { get; set; }

        [Option('t', longName: "SeriesType", HelpText = "Resolution of the data-series.", Required = false, Default = SeriesType.DailySum)]
        public SeriesType SeriesType { get; set; }

        [Option('h', longName: "HumanReadable", HelpText = "Readable Output.", Required = false, Default = true)]
        public bool HumanReadable { get; set; }

        [Option('l', longName: "DeviceClass", HelpText = "Device class to query.", Required = false, Default = DeviceClass.Inverter)]
        public DeviceClass DeviceClass { get; set; }

        [Usage(ApplicationAlias = "poller")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>() {
                    new Example("Fetch archive data from Voltage_AC_Phase_1, Voltage_AC_Phase_2, Voltage_AC_Phase_3 channels and output to the console", 
                    new FetchInverterArchiveDataOptions { StartDate = DateTime.Now.AddDays(-1), EndDate = DateTime.Now,
                        Channels = new List<Channel> { Channel.Voltage_AC_Phase_1, Channel.Voltage_AC_Phase_2, Channel.Voltage_AC_Phase_3 },
                        Store = DataStore.Console
                    })
                };
            }
        }

    }

   
}
