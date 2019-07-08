using System;
using System.Collections.Generic;
using System.Text;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;

namespace FroniusSolarApi.Repository
{
    public class CsvRepository : IDataRepository
    {
        public bool SaveCommonInverterData(CommonInverterData data)
        {
            throw new NotImplementedException();
        }

        public bool SaveCumulationInverterData(CumulationInverterData data)
        {
            throw new NotImplementedException();
        }

        public bool SaveMinMaxInverterData(MinMaxInverterData data)
        {
            throw new NotImplementedException();
        }

        public bool SaveP3InverterData(P3InverterData data)
        {
            throw new NotImplementedException();
        }
    }
}
