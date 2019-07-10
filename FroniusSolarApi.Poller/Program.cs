using FroniusSolarApi.Repository;
using FroniusSolarApi.Repository.Csv;
using FroniusSolarClient;
using FroniusSolarClient.Entities.SolarAPI.V1;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using System;

namespace FroniusSolarApi.Poller
{
    class Program
    {
        static void OutputResponseHeader(CommonResponseHeader responseHeader)
        {
            Console.WriteLine($"{responseHeader.Status.Code} at {responseHeader.Timestamp}");
        }

        static void Main(string[] args)
        {
            RepositoryService service = new RepositoryService(new CsvRepository(new CsvConfiguration(@"C:\Temp","test")));

            var solarClient = new SolarClient("10.1.1.124", 1, OutputResponseHeader);

            var data = solarClient.GetCommonInverterData();

            service.SaveData(data);
        }
    }
}
