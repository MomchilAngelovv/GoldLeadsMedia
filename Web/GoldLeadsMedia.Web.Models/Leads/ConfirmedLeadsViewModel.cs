using System.Collections.Generic;

namespace GoldLeadsMedia.Web.Models.Leads
{
    public class ConfirmedLeadsViewModel
    {
        public IEnumerable<ConfirmedLeadsPartner> Partners { get; set; }
        public IEnumerable<ConfirmedLeadsLead> Leads { get; set; }
    }
}
