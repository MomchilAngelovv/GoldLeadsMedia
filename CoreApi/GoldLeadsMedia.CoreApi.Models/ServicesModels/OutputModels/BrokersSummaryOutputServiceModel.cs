using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels
{
    public class BrokersSummaryOutputServiceModel
    {
        public IEnumerable<BrokersSummaryBroker> Brokers { get; set; }
    }
}
