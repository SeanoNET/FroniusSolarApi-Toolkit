using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FroniusSolarClient.Entities.SolarAPI.V1.ArchiveData;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using FroniusSolarClient.Entities.SolarAPI.V1.PowerFlowRealtimeData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FroniusSolarApi.Repository.Csv
{
    public class CsvRepository : IDataRepository
    {
        private readonly CsvConfiguration _csvConfiguration;

        public CsvRepository(CsvConfiguration config)
        {
            _csvConfiguration = config;
        }
        public CsvRepository(IConfiguration config) 
            :this(new CsvConfiguration(config))
        {
        }

        public bool SaveCommonInverterData(CommonInverterData data)
        {
            var writer = new CsvWriter<CommonInverterData>();
            return writer.WriteCsv(data, _csvConfiguration.GetSaveLocation("CommonInverterData"));
        }

        public bool SaveCumulationInverterData(CumulationInverterData data)
        {
            var writer = new CsvWriter<CumulationInverterData>();
            return writer.WriteCsv(data, _csvConfiguration.GetSaveLocation("CumulationInverterData"));
        }

        public bool SaveMinMaxInverterData(MinMaxInverterData data)
        {
            var writer = new CsvWriter<MinMaxInverterData>();
            return writer.WriteCsv(data, _csvConfiguration.GetSaveLocation("MinMaxInverterData"));
        }

        public bool SaveP3InverterData(P3InverterData data)
        {
            var writer = new CsvWriter<P3InverterData>();
            return writer.WriteCsv(data, _csvConfiguration.GetSaveLocation("P3InverterData"));
        }

        public bool SaveArchiveData(Dictionary<string, ArchiveData> data)
        {
            var writer = new CsvWriter<Dictionary<string, ArchiveData>>();
            return writer.WriteCsv(data, _csvConfiguration.GetSaveLocation("ArchiveData"));
        }

        public bool SavePowerFlowRealtimeData(PowerFlowRealtimeData data)
        {
            var writer = new CsvWriter<PowerFlowRealtimeData>();
            return writer.WriteCsv(data, _csvConfiguration.GetSaveLocation("PowerFlowRealtimeData"));
        }
    }
}
