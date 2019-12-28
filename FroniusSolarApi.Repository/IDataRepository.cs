using FroniusSolarClient.Entities.SolarAPI.V1.ArchiveData;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using FroniusSolarClient.Entities.SolarAPI.V1.PowerFlowRealtimeData;
using System;
using System.Collections.Generic;

namespace FroniusSolarApi.Repository
{
    public interface IDataRepository
    {
        bool SaveCumulationInverterData(CumulationInverterData data);

        bool SaveCommonInverterData(CommonInverterData data);

        bool SaveP3InverterData(P3InverterData data);

        bool SaveMinMaxInverterData(MinMaxInverterData data);

        bool SaveArchiveData(Dictionary<string, ArchiveData> data);

        bool SavePowerFlowRealtimeData(PowerFlowRealtimeData data);
    }
}
