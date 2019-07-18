using FroniusSolarApi.Poller.CLI;
using FroniusSolarApi.Repository;
using FroniusSolarApi.Repository.ConsoleOut;
using FroniusSolarApi.Repository.Csv;
using FroniusSolarClient;
using FroniusSolarClient.Entities.SolarAPI.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public PollerApp(ILogger<PollerApp> logger)
        {
            _logger = logger;
            config = ConfigurationBuild();

            // Configure Solar client
            // TODO: Load from app settings
            _solarClient = new SolarClient("10.1.1.124", 1, OutputResponseHeader);                
        }

        private IConfiguration ConfigurationBuild()
        {
            _logger.LogInformation("Building configuration");
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\appsettings.json"))
                _logger.LogWarning("No appsettings.json found");

            // Load from configuration
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);

            return configBuilder.Build();
        }

        private void ConfigureRepository(DataStore store)
        {
            _logger.LogInformation($"Configuring {store} repository");

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

        void OutputResponseHeader(CommonResponseHeader responseHeader)
        {
            _logger.LogInformation($"Response status: {responseHeader.Status.Code} at {responseHeader.Timestamp}");
        }

        public int FetchAndSave(FetchOptions opt)
        {
            ConfigureRepository(opt.Store);

            bool result = false;

            switch (opt.Collections)
            {
                case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.CumulationInverterData:
                    var cumulationInverterData = _solarClient.GetCumulationInverterData();
                    _logger.LogInformation($"Fetched CumulationInverterData");
                    result = _repositoryService.SaveData(cumulationInverterData);
                    break;
                case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.CommonInverterData:
                    var commonInverterData = _solarClient.GetCommonInverterData();
                    _logger.LogInformation($"Fetched CommonInverterData");
                    result = _repositoryService.SaveData(commonInverterData);
                    break;
                case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.MinMaxInverterData:
                    var minMaxInverterData = _solarClient.GetMinMaxInverterData();
                    _logger.LogInformation($"Fetched MinMaxInverterData");
                    result = _repositoryService.SaveData(minMaxInverterData);
                    break;
                default:
                    break;
            }

            if (result)
            {
                _logger.LogInformation($"Saved successfully");
                return 0;
            }
            else
            {
                _logger.LogError($"Save was unsuccessful");
                return 1;
            }

           
        }
    }
}
