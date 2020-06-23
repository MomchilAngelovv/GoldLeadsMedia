using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.CoreApi.Models.ServicesModels.OutputModels
{
    public class BrokersSummaryBroker
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalLeads { get; set; }
        public int TotalFtds { get; set; }
    }
}
