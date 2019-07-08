using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
using System;

namespace FroniusSolarApi.Repository
{
    public interface IDataRepository
    {
        bool SaveCumulationInverterData(CumulationInverterData data);

        bool SaveCommonInverterData(CommonInverterData data);

        bool SaveP3InverterData(P3InverterData data);

        bool SaveMinMaxInverterData(MinMaxInverterData data);
    }
}
