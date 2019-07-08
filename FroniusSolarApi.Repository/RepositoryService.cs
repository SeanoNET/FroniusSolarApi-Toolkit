using FroniusSolarClient.Entities.SolarAPI.V1.InverterRealtimeData;
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


        public bool SaveData(object data)
        {
            return false;
        }
    }
}
