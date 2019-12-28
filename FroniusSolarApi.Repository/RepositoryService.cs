using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using FroniusSolarClient.Entities.SolarAPI.V1.ArchiveData;
using FroniusSolarClient.Entities.SolarAPI.V1.PowerFlowRealtimeData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FroniusSolarApi.Repository
{
    public class RepositoryService 
    {
        private readonly IDataRepository _dataRepository;

        public RepositoryService(IDataRepository dataRepository)
        {
            this._dataRepository = dataRepository;
        }

        public bool SaveData(CumulationInverterData data)
        {
            return _dataRepository.SaveCumulationInverterData(data);
        }

        public bool SaveData(CommonInverterData data)
        {
            return _dataRepository.SaveCommonInverterData(data);
        }

        public bool SaveData(P3InverterData data)
        {
            return _dataRepository.SaveP3InverterData(data);
        }

        public bool SaveData(MinMaxInverterData data)
        {
            return _dataRepository.SaveMinMaxInverterData(data);
        }

        public bool SaveData(Dictionary<string, ArchiveData> data)
        {
            return _dataRepository.SaveArchiveData(data);
        }

        public bool SaveData(PowerFlowRealtimeData data)
        {
            return _dataRepository.SavePowerFlowRealtimeData(data);
        }
    }
}
