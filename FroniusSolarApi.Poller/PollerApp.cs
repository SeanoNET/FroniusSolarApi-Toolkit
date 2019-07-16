using FroniusSolarApi.Poller.CLI;
using FroniusSolarApi.Repository;
using FroniusSolarApi.Repository.ConsoleOut;
using FroniusSolarClient;
using FroniusSolarClient.Entities.SolarAPI.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace FroniusSolarApi.Poller
{
    internal class PollerApp
    {
        private RepositoryService _repositoryService;
        private readonly SolarClient _solarClient;

        public PollerApp()
        {
            // Configure Solar client
            // TODO: Load from app settings
            _solarClient = new SolarClient("10.1.1.124", 1);

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

            var data = _solarClient.GetCommonInverterData();

            _repositoryService.SaveData(data);

            return 0;
        }
    }
}
