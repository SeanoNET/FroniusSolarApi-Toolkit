using FroniusSolarApi.Poller.CLI;
using FroniusSolarApi.Repository;
using FroniusSolarApi.Repository.ConsoleOut;
using FroniusSolarApi.Repository.Csv;
using FroniusSolarClient;
using FroniusSolarClient.Entities.SolarAPI.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FroniusSolarApi.Poller
{
    internal class PollerApp
    {
        private RepositoryService _repositoryService;
        private readonly SolarClient _solarClient;

        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public PollerApp(ILogger<PollerApp> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            // Configure Solar client
            // TODO: Load from app settings
            _solarClient = new SolarClient(config.GetSection("SolarAPI_URL").Value, 1, logger);                
        }

        private bool ConfigureRepository(DataStore store)
        {
            _logger.LogInformation($"Configuring {store} repository");

            try
            {
                switch (store)
                {
                    case DataStore.Console:
                        // Configure the repository service to output to console
                        _repositoryService = new RepositoryService(new ConsoleRepository());
                        break;
                    case DataStore.Csv:
                        // Configure the repository service to output to a csv file
                        _repositoryService = new RepositoryService(new CsvRepository(_config));
                        break;
                    case DataStore.Mssql:
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to configure repository.");
                return false;
            }

        }

        public int FetchAndSaveInverterRealtimeData(FetchInverterRealtimeDataOptions opt)
        {
            if (ConfigureRepository(opt.Store))
            {
                bool result = false;
                try
                {
                    switch (opt.Collections)
                    {
                        case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.CumulationInverterData:
                            var cumulationInverterResponse = _solarClient.GetCumulationInverterData(opt.DeviceId, opt.Scope);
                            _logger.LogInformation($"Fetched CumulationInverterData - Status: {cumulationInverterResponse.Head.Status.Code} at {cumulationInverterResponse.Head.Timestamp}");
                            result = _repositoryService.SaveData(cumulationInverterResponse.Body.Data);
                            break;
                        case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.CommonInverterData:
                            var commonInverterResponse = _solarClient.GetCommonInverterData(opt.DeviceId, opt.Scope);
                            _logger.LogInformation($"Fetched CommonInverterData - Status: {commonInverterResponse.Head.Status.Code} at {commonInverterResponse.Head.Timestamp}");
                            result = _repositoryService.SaveData(commonInverterResponse.Body.Data);
                            break;
                        case FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData.DataCollection.MinMaxInverterData:
                            var minMaxInverterResponse = _solarClient.GetMinMaxInverterData(opt.DeviceId, opt.Scope);
                            _logger.LogInformation($"Fetched MinMaxInverterData - Status: {minMaxInverterResponse.Head.Status.Code} at {minMaxInverterResponse.Head.Timestamp}");
                            result = _repositoryService.SaveData(minMaxInverterResponse.Body.Data);
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
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured.");
                    return 1;
                }
            }
            else
            {
                // Configure repository failed
                return 1;
            }        
        }

        public int FetchAndSaveArchiveData(FetchInverterArchiveDataOptions opt)
        {
            if (ConfigureRepository(opt.Store))
            {
                bool result = false;
                try
                {
                    var archiveResponse = _solarClient.GetArchiveData(opt.StartDate, opt.EndDate, opt.Channels.Cast<Channel>().ToList(), opt.DeviceId, opt.Scope, opt.SeriesType, opt.HumanReadable, opt.DeviceClass);
                    _logger.LogInformation($"Fetched archived data - Status: {archiveResponse.Head.Status.Code} at {archiveResponse.Head.Timestamp}");

                    result = _repositoryService.SaveData(archiveResponse.Body.Data);




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
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured.");
                    return 1;
                }
            }
            else
            {
                // Configure repository failed
                return 1;
            }        
        }

        public int FetchAndSavePowerFlowRealtimeData(FetchPowerFlowRealtimeDataOptions opt)
        {
            if (ConfigureRepository(opt.Store))
            {
                bool result = false;
                try
                {
                    var powerFlowRealtimeDataResponse = _solarClient.GetPowerFlowRealtimeData();
                    _logger.LogInformation($"Fetched PowerFlowRealtimeData - Status: {powerFlowRealtimeDataResponse.Head.Status.Code} at {powerFlowRealtimeDataResponse.Head.Timestamp}");
                    result = _repositoryService.SaveData(powerFlowRealtimeDataResponse.Body.Data);

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
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occured.");
                    return 1;
                }
            }
            else
            {
                // Configure repository failed
                return 1;
            }
        }
    } 
}
