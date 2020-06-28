namespace GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels
{
    using System.Collections.Generic;

    public class BrokersSummaryOutputServiceModel
    {
        public IEnumerable<BrokersSummaryBroker> Brokers { get; set; }
    }
}
