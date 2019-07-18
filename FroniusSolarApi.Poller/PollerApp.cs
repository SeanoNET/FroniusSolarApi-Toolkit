using FroniusSolarApi.Poller.CLI;
using FroniusSolarApi.Repository;
using FroniusSolarApi.Repository.ConsoleOut;
using FroniusSolarApi.Repository.Csv;
using FroniusSolarClient;
using FroniusSolarClient.Entities.SolarAPI.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FroniusSolarApi.Poller
{
    internal class PollerApp
    {
        private RepositoryService _repositoryService;
        private readonly SolarClient _solarClient;

        private readonly IConfiguration config;


        public PollerApp()
        {
            // Configure Solar client
            // TODO: Load from app settings
            _solarClient = new SolarClient("10.1.1.124", 1);

            config = ConfigurationBuild();
        }

        private IConfiguration ConfigurationBuild()
        {
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\appsettings.json"))
                Console.WriteLine("WARNING - No appsettings.json found");

            // Load from configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            return configBuilder.Build();
        }

        private void ConfigureRepository(DataStore store)
        {
            switch (store)
            {
                case DataStore.Console:
                    // Configure the repository service to output to console
                    _repositoryService = new RepositoryService(new ConsoleRepository());
                    break;
                case DataStore.Csv:
                    // Configure the repository service to output to a csv file
                    _repositoryService = new RepositoryService(new CsvRepository(config));
                    break;
                case DataStore.Mssql:
                    break;
                default:
                    break;
            }
        }

        static void OutputResponseHeader(CommonResponseHeader responseHeader)
        {
            Console.WriteLine($"{responseHeader.Status.Code} at {responseHeader.Timestamp}");
        }


        public int FetchAndSave(FetchOptions opt)
        {
            Console.WriteLine($"Saving collection data {opt.Collections} to {opt.Store}");

            ConfigureRepository(opt.Store);

            bool result = false;

            switch (opt.Collections)
            {
                case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.CumulationInverterData:
                    var cumulationInverterData = _solarClient.GetCumulationInverterData();
                    result = _repositoryService.SaveData(cumulationInverterData);
                    break;
                case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.CommonInverterData:
                    var commonInverterData = _solarClient.GetCommonInverterData();
                    result = _repositoryService.SaveData(commonInverterData);
                    break;
                case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.MinMaxInverterData:
                    var minMaxInverterData = _solarClient.GetMinMaxInverterData();
                    result = _repositoryService.SaveData(minMaxInverterData);
                    break;
                default:
                    break;
            }

            if (result)
            {
                Console.WriteLine("Data saved successfully");
                return 0;
            }
            else
            {
                Console.WriteLine("ERROR - Data save was unsuccessful");
                return 1;
            }

           
        }
    }
}
