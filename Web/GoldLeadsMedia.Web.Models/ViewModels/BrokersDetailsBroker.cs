using System;
using System.Collections.Generic;
using System.Text;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class BrokersDetailsBroker
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<BrokersDetailsBrokerLead> Leads { get; set; }

        public string CreatedOn { get; set; }
        public string DeletedOn { get; set; }
        public string Information { get; set; }
    }
}
