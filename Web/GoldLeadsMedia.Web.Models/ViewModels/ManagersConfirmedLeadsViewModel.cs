using System.Collections.Generic;

namespace GoldLeadsMedia.Web.Models.ViewModels
{
    public class ManagersConfirmedLeadsViewModel
    {
        public IEnumerable<ManagersConfirmedLeadsPartner> Partners { get; set; }
        public IEnumerable<ManagersConfirmedLeadsLead> Leads { get; set; }
    }
}
