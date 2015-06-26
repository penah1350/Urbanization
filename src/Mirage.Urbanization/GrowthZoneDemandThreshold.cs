﻿using System.Collections.Generic;
using System.Linq;
using Mirage.Urbanization.ZoneConsumption.Base;

namespace Mirage.Urbanization
{
    internal class GrowthZoneDemandThreshold<TDemandingConsumption, TDemandedConsumption> : IGrowthZoneDemandThreshold
        where TDemandingConsumption : BaseZoneClusterConsumption
        where TDemandedConsumption : BaseZoneClusterConsumption
    {
        private readonly string _onExceededMessage;
        private int _availableConsumptions;

        public GrowthZoneDemandThreshold(IEnumerable<TDemandedConsumption> currentlyOffered, string onExceededMessage, int growthFactor)
        {
            _onExceededMessage = onExceededMessage;
            _availableConsumptions = growthFactor + (new HashSet<TDemandedConsumption>(currentlyOffered.Where(x => x.HasPower)).Count * growthFactor);
        }

        public bool DecrementAvailableConsumption(BaseZoneClusterConsumption baseZoneClusterConsumption)
        {
            if (baseZoneClusterConsumption is TDemandingConsumption)
            {
                _availableConsumptions--;
                return true;
            }
            return false;
        }

        public string OnExceededMessage { get { return _onExceededMessage; } }

        public bool AvailableConsumptionsExceeded
        {
            get
            {
                return _availableConsumptions <= 0;
            }
        }
    }
}