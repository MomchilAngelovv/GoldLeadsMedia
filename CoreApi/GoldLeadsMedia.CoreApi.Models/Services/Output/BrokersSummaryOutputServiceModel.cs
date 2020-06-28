namespace GoldLeadsMedia.CoreApi.Models.Services.Output
{
    using System.Collections.Generic;

    public class BrokersSummaryOutputServiceModel
    {
        public IEnumerable<BrokersSummaryBroker> Brokers { get; set; }
    }
}
