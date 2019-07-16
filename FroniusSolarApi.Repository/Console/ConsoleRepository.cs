using System;
using System.Collections.Generic;
using System.Text;
using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;

namespace FroniusSolarApi.Repository.ConsoleOut
{
    public class ConsoleRepository : IDataRepository
    {

        public bool SaveCommonInverterData(CommonInverterData data)
        {
            var writer = new ConsoleWriter<CommonInverterData>();
            writer.WriteConsole(data);
            return true;
        }

        public bool SaveCumulationInverterData(CumulationInverterData data)
        {
            var writer = new ConsoleWriter<CumulationInverterData>();
            writer.WriteConsole(data);
            return true;
        }

        public bool SaveMinMaxInverterData(MinMaxInverterData data)
        {
            var writer = new ConsoleWriter<MinMaxInverterData>();
            writer.WriteConsole(data);
            return true;
        }

        public bool SaveP3InverterData(P3InverterData data)
        {
            var writer = new ConsoleWriter<P3InverterData>();
            writer.WriteConsole(data);
            return true;
        }
    }
}
