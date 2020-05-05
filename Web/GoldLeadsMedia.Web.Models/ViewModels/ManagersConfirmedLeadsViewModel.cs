namespace GoldLeadsMedia.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class ManagersConfirmedLeadsViewModel
    {
        public IEnumerable<ManagersConfirmedLeadsPartner> Partners { get; set; }
        public IEnumerable<ManagersConfirmedLeadsLead> ConfirmedLeads { get; set; }
    }
}
